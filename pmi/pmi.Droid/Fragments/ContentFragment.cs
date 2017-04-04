using System;
using System.Linq;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using pmi.Core.Views.Content;
using pmi.Core.Views;
using MvvmCross.Droid.Shared.Attributes;
using Android.Webkit;
using Java.Lang;
using pmi.Core.Views.Menu;
using pmi.Droid.Activities;
using static Android.Webkit.WebSettings;
using String = System.String;

namespace pmi.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("pmi.droid.fragments.ContentFragment")]
    public class ContentFragment : BaseFragment<ContentViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_content;

        private WebView _webview;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            
            _webview = view.FindViewById<WebView>(Resource.Id.webView);
            _webview.SetWebViewClient(new WebViewClient());
            _webview.Settings.JavaScriptEnabled = true;
            //_webview.SetFitsSystemWindows(true);
            _webview.SetWebViewClient(new Utilities.WebViewClient(view.FindViewById<ProgressBar>(Resource.Id.prgbar_webview), OnPageLoadingDone));
            _webview.Settings.DefaultZoom = ZoomDensity.Far;
            _webview.Alpha = 0;
            _webview.Settings.AllowUniversalAccessFromFileURLs = true;

            _webview.SetDownloadListener(new Utilities.WebViewDownloader(Context));
            
            if (MenuViewModel.MenuItems?.Count > 0)
            {
                _webview.LoadUrl(MenuViewModel.MenuItems.First().url);
            }

            return view;
        }
        
        public override void OnClickedMenu(Utilities.Events.ToolbarClickListener.CLICK_STATUS status)
        {
            if (status == Utilities.Events.ToolbarClickListener.CLICK_STATUS.GO_BACK) {
                if (_webview.CanGoBack())
                {
                    _webview.GoBack();
                }
                else
                {
                    ((MainActivity)Activity).DisplayIconNavigateOnMenu();
                }
            }

            OnPageLoadingDone(_webview.Url);
        }

        private void OnPageLoadingDone(string url)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)) return;

            if (!MenuViewModel.IsUrlContainedInMenu(url))
            {
                ((MainActivity)Activity).DisplayBackArrowOnMenu();
            }
            else
            {
                ((MainActivity)Activity).DisplayIconNavigateOnMenu();
            }
        }                
    }
}