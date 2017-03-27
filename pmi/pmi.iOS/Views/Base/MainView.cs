using Foundation;
using pmi.Core.Views;
using MvvmCross.iOS.Support.SidePanels;

namespace pmi.iOS.Views
{
    [Register("MainView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class MainView : BaseViewController<MainViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.ShowMenu();
        }
    }   
}