using System;
using pmi.Core.Services;
using pmi.iOS.Utilities;
using UIKit;

namespace pmi.iOS.Views
{
    public partial class SplashScreenView : UIViewController
    {
        public SplashScreenView() : base("SplashScreenView", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Layout.InjectMainBackground(this.View.Bounds);
            Layout.AdaptSizeToScreen();
            
            MainTitle.Text = AppInfo.NAME;

            MainTitle.TextContainer.LineFragmentPadding = 0;
            MainTitle.ApplyTitleStyle();

            MainTitle.TextContainerInset = new UIEdgeInsets((MainTitle.Bounds.Height / 2) - (MainTitle.ContentSize.Height / 2), (nfloat)LangTableRow.PADDING_LEFT, 0, 0);

            (new Spinner(Layout)).Display();
        }
    }
}