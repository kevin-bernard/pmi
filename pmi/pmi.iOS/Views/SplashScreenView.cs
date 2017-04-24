using Foundation;
using System;
using pmi.iOS.Utilities;
using UIKit;

namespace pmi.iOS
{
    public partial class SplashScreenView : UIViewController
    {
        public SplashScreenView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Layout.AdaptSizeToScreen();

            UIImageView imgView = new UIImageView(UIImage.FromBundle("launch_screen"));
            
            Layout.InsertSubview(imgView, 0);

            (new Spinner(Layout)).Display();
        }
    }
}