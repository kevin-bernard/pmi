using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Foundation;
using MvvmCross.iOS.Support.SidePanels;
using pmi.Core;
using pmi.Core.Services;
using pmi.Core.Views;
using pmi.iOS.Utilities;
using UIKit;
using Xamarin.Forms;
using Style = pmi.iOS.Utilities.Style;

namespace pmi.iOS.Views.Base
{
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public partial class MainView : BaseViewController<MainViewModel>
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UITextView MainTitle { get; set; }
        
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

                alert.Clicked += (object s, UIButtonEventArgs ev) => {
                    Process.GetCurrentProcess().Kill();
                    NSThread.Exit();
                };

                alert.Show();
            }

        }
    }
}