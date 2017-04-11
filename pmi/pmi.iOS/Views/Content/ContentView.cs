using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using Foundation;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.Platform;
using pmi.Core.Views.Content;
using pmi.Core.Views.Menu;
using pmi.iOS.Utilities;
using pmi.iOS.Utilities.Contracts;
using pmi.iOS.Views;
using UIKit;
using WebKit;

namespace pmi.iOS.Views
{
    [Register("ContentView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class ContentView : BaseViewController<ContentViewModel>, IMenuListener, IWebViewListener
    {
        UIWebView WebView { get; set; }

        private UILabel Title { get; set; }

        private UIButton GoBackButton { get; set; }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Title =
                new UILabel(new CGRect(60, 0, NavigationController.Toolbar.Frame.Width,
                    NavigationController.Toolbar.Frame.Height))
                {
                    TextColor = Style.Header.TextColor,
                    BackgroundColor = UIColor.Clear
                };
            
            GoBackButton = new UIButton(new CGRect(0, 0, 60, NavigationController.Toolbar.Frame.Height))
            {
                TintColor = Style.Header.TextColor,
                Hidden = true
            };

            GoBackButton.TouchDown += OnClickGoBack;

            GoBackButton.SetImage(UIImage.FromBundle("back").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);

            NavigationController.NavigationBar.AddSubviews(GoBackButton);
            
            NavigationController.NavigationBar.TintColor = Style.Header.TextColor;
            NavigationController.NavigationBar.BarTintColor = Style.Header.BackgroundColor;

            NavigationController.NavigationBar.AddSubviews(Title);
            
            var scrollView = new UIScrollView(View.Frame)
            {
                ShowsHorizontalScrollIndicator = false,
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            };

            WebView = new UIWebView
            {
                ScalesPageToFit = true
            };

            WebView.Delegate = new WebViewDelegate(View, this);
            
            scrollView.AddSubviews(WebView);

            Add(scrollView);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                scrollView.AtLeftOf(View),
                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.WithSameHeight(View),
                
                WebView.AtLeftOf(scrollView),
                WebView.AtTopOf(scrollView),
                WebView.WithSameWidth(scrollView),
                WebView.WithSameHeight(scrollView));

            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            
            MenuClickEvent.AddListener(this);

            var menuItem = MenuViewModel.MenuItems.First();

            OnMenuItemClick(menuItem.menu_title, menuItem.url);
        }

        public void OnMenuItemClick(string title, string url)
        {
            WebView.LoadRequest(new NSUrlRequest(new NSUrl(url)));

            Mvx.Resolve<IMvxSideMenu>().Close();

            NavigationController.Title = title;
            
            Title.Text = title;
        }

        public void OnClickGoBack(object sender, EventArgs e)
        {
            WebView.GoBack();
        }

        public void OnWebViewFinishLoading(UIWebView webview)
        {
            var urlContainedInMenu = MenuViewModel.IsUrlContainedInMenu(webview.Request.Url.ToString());

            NavigationController.NavigationBar.TintColor = urlContainedInMenu ? Style.Header.TextColor : UIColor.Clear;
            
            GoBackButton.Hidden = urlContainedInMenu;
        }
    }
}