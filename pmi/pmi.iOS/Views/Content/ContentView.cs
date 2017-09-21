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
using System.IO;
using pmi.Core.Services;
using pmi.Utilities;
using System.Collections.Generic;
using System.Text;

namespace pmi.iOS.Views
{
    [Register("ContentView")]
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class ContentView : BaseViewController<ContentViewModel>, IMenuListener, IWebViewListener
    {
        UIWebView WebView { get; set; }

        private UILabel Title { get; set; }

        private UIButton GoBackButton { get; set; }

        private UIView GalleryView { get; set; }

        private UIButton ChoosePicture { get; set; }

        private UIButton TakePicture { get; set; }

        private Spinner _spinner;

        private UIImagePickerController imgPicker;

        private System.Collections.Generic.Dictionary<int, string> indexImage = new System.Collections.Generic.Dictionary<int, string>() {
            {0, "home_page.html" },

            {3, "sanctuary_map.html" }
        };

        private byte[] ImageToUpload { get; set; }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (ImageToUpload != null)
            {

                ImageToUpload = null;
            }
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

            GalleryView = new UIView() {
                Hidden = true
            };

            ChoosePicture = UIButton.FromType(UIButtonType.RoundedRect);
            ChoosePicture.Frame =  new CGRect(0, 0, View.Frame.Width / 2, 40);

            ChoosePicture.TintColor = Style.Header.TextColor;
            ChoosePicture.BackgroundColor = UIColor.LightGray;

            ChoosePicture.SetTitle(Translator.GetText("choose_picture"), UIControlState.Normal);

            ChoosePicture.TouchDown += OnClickChoosePicture;

            TakePicture = UIButton.FromType(UIButtonType.RoundedRect);
            TakePicture.Frame =  new CGRect(View.Frame.Width / 2, 0, View.Frame.Width / 2, 40);

            TakePicture.TintColor = Style.Header.TextColor;
            TakePicture.BackgroundColor = UIColor.LightGray;

            TakePicture.SetTitle(Translator.GetText("take_picture"), UIControlState.Normal);

            TakePicture.TouchDown += OnClickTakePicture;

            GalleryView.AddSubviews(ChoosePicture, TakePicture);

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

            scrollView.AddSubviews(WebView, GalleryView);

            Add(scrollView);
            
            View.AddConstraints(
                scrollView.AtLeftOf(View),
                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.WithSameHeight(View),
                
                WebView.AtLeftOf(scrollView),
                WebView.AtTopOf(scrollView),
                WebView.WithSameWidth(scrollView),
                WebView.WithSameHeight(scrollView),

                GalleryView.AtLeftOf(scrollView),
                GalleryView.AtTopOf(scrollView),
                GalleryView.WithSameWidth(scrollView),
                GalleryView.WithSameHeight(scrollView),
                
                TakePicture.AtLeftOf(GalleryView),
                TakePicture.AtTopOf(GalleryView),

                ChoosePicture.AtRightOf(GalleryView),
                ChoosePicture.AtTopOf(GalleryView)
            );

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            
            MenuClickEvent.AddListener(this);

            var menuItem = MenuViewModel.MenuItems.First();

            OnMenuItemClick(menuItem.menu_title, menuItem.url, 0);

            _spinner = new Spinner(View);

            WebView.Delegate = new WebViewDelegate(_spinner, this);

        }

        public void OnMenuItemClick(string title, string url, int index)
        {
            WebView.Hidden = false;
            GalleryView.Hidden = true;

            if (url == string.Empty)
            {
                ShowGalleryContent();
            }
            else
            {
                var nsUrl = new NSUrl(url);

                if (indexImage.ContainsKey(index))
                {
                    nsUrl = new NSUrl(Path.Combine(NSBundle.MainBundle.BundlePath, "Content/" + indexImage[index]), false);
                }

                WebView.LoadRequest(new NSUrlRequest(nsUrl));
            }
            
            Mvx.Resolve<IMvxSideMenu>().Close();

            NavigationController.Title = title;

            Title.Text = title;
        }

        public void ShowGalleryContent() {

            WebView.Hidden = true;
            GalleryView.Hidden = false;
            _spinner.Display();

            ViewModel.GetGaleryImages(((List<GalleryImage> images) => {

                InvokeOnMainThread(() =>
                {
                    DisplayGallery(images);
                });

            }));
            
        }

        public void DisplayGallery(List<GalleryImage> images) {

            int offsetLeft = 10,
                offsetTop = 5;

            int left = offsetLeft,
                top = (int)TakePicture.Frame.Height + 5,
                imgHeight = 50, imgWidth = 50,
                frameWidth = (int)View.Frame.Width;


            Task.Run(() =>
            {

                foreach (var img in images)
                {
                    var uiImg = UIImage.LoadFromData(NSData.FromUrl(new NSUrl(img.url)));

                    if (left + imgWidth + offsetLeft > frameWidth)
                    {
                        left = offsetLeft;
                        top += imgHeight + offsetTop;
                    }

                    InvokeOnMainThread(() =>
                    {
                        UIButton btn = new UIButton(new CGRect(left, top, imgWidth, imgHeight));
                        btn.SetImage(uiImg, UIControlState.Normal);
                        btn.TouchUpInside += ((object sender, EventArgs e) =>
                        {
                            var imgToDisplay = ((UIButton)sender).ImageView.Image;

                            var btnImg = UIButton.FromType(UIButtonType.Custom);
                            btnImg.BackgroundColor = UIColor.Black;
                            btnImg.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
                            btnImg.SetImage(imgToDisplay, UIControlState.Normal);

                            btnImg.TouchUpInside += ((object sender2, EventArgs e2) => {
                                NavigationController.DismissModalViewController(true);
                            });

                            UIViewController ctrl = new UIViewController();
                            ctrl.View.Add(btnImg);

                            NavigationController.PresentModalViewController(ctrl, true);
                        });
                        
                        GalleryView.AddSubview(btn);
                    });

                    left += imgWidth + offsetLeft;
                }

                InvokeOnMainThread(() => {
                    _spinner.Hide();
                });

            });
        }

        public void OnClickGoBack(object sender, EventArgs e)
        {
            if (GalleryView.Hidden)
            {
                WebView.GoBack();
            }  
            else
            {
                var menuItem = MenuViewModel.MenuItems.Find(m => m.url.Equals(WebView.Request.Url));

                if (menuItem != null)
                {
                    OnMenuItemClick(menuItem.menu_title, menuItem.url, MenuViewModel.MenuItems.IndexOf(menuItem));
                }
                
            }
        }

        public void OnClickChoosePicture(object sender, EventArgs e)
        {
            ChoosePicture.BackgroundColor = UIColor.Gray;

            UIView.Animate(0.4, () =>
            {
                ChoosePicture.BackgroundColor = UIColor.LightGray;
            });

            imgPicker = new UIImagePickerController();
            imgPicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            imgPicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            imgPicker.FinishedPickingMedia += OnFinishedPickingMedia;

            NavigationController.PresentViewController(imgPicker, false, null);

            imgPicker.Canceled += ((object o, EventArgs eCanceled) =>
            {
                InvokeOnMainThread(() => {
                    
                    NavigationController.DismissViewControllerAsync(false);
                    //imgPicker.DismissModalViewController(true);
                });
            });
        }

        public void OnClickTakePicture(object sender, EventArgs e)
        {
            TakePicture.BackgroundColor = UIColor.Gray;

            UIView.Animate(0.4, () =>
            {
                TakePicture.BackgroundColor = UIColor.LightGray;
            });

            if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                imgPicker = new UIImagePickerController();
                imgPicker.SourceType = UIImagePickerControllerSourceType.Camera;
                imgPicker.Delegate = new CameraDelegate((() => {
                    var i = 0;
                }));

                PresentViewController(imgPicker, true, null);
            }

        }

        public void OnWebViewFinishLoading(UIWebView webview)
        {
            var urlContainedInMenu = MenuViewModel.IsUrlContainedInMenu(webview.Request.Url.ToString()) || WebView.Request.Url.IsFileUrl;

            NavigationController.NavigationBar.TintColor = urlContainedInMenu ? Style.Header.TextColor : UIColor.Clear;
            
            GoBackButton.Hidden = urlContainedInMenu;
        }

        public async void OnFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            if(e.Info[UIImagePickerController.MediaType].ToString() == "public.image")
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;

                if (originalImage != null)
                {
                    
                    InvokeOnMainThread(() =>
                    {
                        imgPicker.DismissModalViewController(true);
                        _spinner.Display();
                        Toaster.Make(View, Translator.GetText("upload_picture"));
                    });
                    
                    NSData imageData = originalImage.AsJPEG(0.5f);

                    await SendPicture(imageData);
                    
                    
                }
            }
        }

        private async Task SendPicture(NSData imageData) {

            await Task.Delay(100);

            if (imageData.Length > 0)
            {
                byte[] myByteArray = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, myByteArray, 0, Convert.ToInt32(imageData.Length));

                ViewModel.SendPicture(myByteArray, (string res) =>
                {
                    InvokeOnMainThread(() =>
                    {
                        Toaster.Make(View, Translator.GetText("send_picture_success"));
                        _spinner.Hide();
                    });

                });
            }
        }
    }
}