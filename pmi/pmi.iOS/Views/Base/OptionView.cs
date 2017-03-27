using Foundation;
using pmi.Core.Views;
using MvvmCross.iOS.Support.SidePanels;
using Xamarin.Forms;
using pmi.Core.Utilities;

namespace pmi.iOS.Views
{
    [Register("OptionView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class OptionView : BaseViewController<OptionViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var langs = ViewModel.Langs;

            foreach (Lang lang in langs) {

            }
        }
    }   
}