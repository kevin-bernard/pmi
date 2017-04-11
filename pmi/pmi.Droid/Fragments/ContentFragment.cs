using System;
using System.Linq;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using pmi.Core.Views.Content;
using pmi.Core.Views;
using MvvmCross.Droid.Shared.Attributes;
using Android.Webkit;
using Java.Lang;
using pmi.Core.Views.Menu;
using pmi.Droid.Activities;
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
            _webview.Alpha = 0;
            _webview.SetInitialScale(50);
            _webview.LayoutParameters = new RelativeLayout.LayoutParams(Resources.DisplayMetrics.WidthPixels, ViewGroup.LayoutParams.MatchParent);

            _webview.SetWebViewClient(new Utilities.WebViewClient(view.FindViewById<ProgressBar>(Resource.Id.prgbar_webview), OnPageLoadingDone));
            _webview.SetDownloadListener(new Utilities.WebViewDownloader(Context));
            
            ////////////////////////////////////////////////////////////////////
            //_webview.Settings.DefaultZoom = ZoomDensity.Far;
            
            _webview.Settings.LoadWithOverviewMode = true;
            _webview.Settings.UseWideViewPort = true;
            _webview.Settings.JavaScriptEnabled = true;
            _webview.ScrollbarFadingEnabled = false;
            _webview.Settings.AllowUniversalAccessFromFileURLs = true;
            _webview.Settings.BuiltInZoomControls = true;
            _webview.Settings.DisplayZoomControls = false;
            _webview.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            
            ////////////////////////////////////////////////////////////////////
             
            

            
            
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