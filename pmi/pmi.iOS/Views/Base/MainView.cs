using CoreGraphics;
using Foundation;
using pmi.Core.Views;
using MvvmCross.iOS.Support.SidePanels;
using pmi.Core;
using UIKit;

namespace pmi.iOS.Views
{
    [Register("MainView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class MainView : BaseViewController<MainViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (AppManager.CurrentApplication.Error == null)
            {
                ViewModel.ShowMenu();
            }
            else
            {
                UIAlertView alert = new UIAlertView();

                alert.Title = "Error";

                alert.AddButton("OK");

                alert.Message = AppManager.CurrentApplication.Error.Message;

                alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
                alert.Clicked += (object s, UIButtonEventArgs ev) => {
                    NSThread.Exit();
                };

                alert.Show();
            }
            
        }
    }   
}