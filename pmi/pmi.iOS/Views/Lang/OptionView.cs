using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using pmi.Core.Views;
using MvvmCross.iOS.Support.SidePanels;
using pmi.Core.Services;
using pmi.Core.Utilities;
using pmi.Core.Views.Menu;
using pmi.iOS.Services;
using pmi.iOS.Utilities;
using UIKit;

namespace pmi.iOS.Views.Lang
{
    [MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public partial class OptionView : BaseViewController<OptionViewModel>
    {
        private Spinner _spinner;

       //public OptionView() : base("OptionView", null)
       //{
       //}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.InjectMainBackground(this.View.Bounds);
            View.AdaptSizeToScreen();

            NavigationController.SetNavigationBarHidden(true, true);

            MainTitle.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, TableView.Frame.Top);


            MainTitle.TextContainer.LineFragmentPadding = 0;
            MainTitle.Text = string.Empty;
            MainTitle.ApplyTitleStyle();

            //MainTitle.TextContainerInset = new UIEdgeInsets((MainTitle.Bounds.Height / 2) - 8, (nfloat)LangTableRow.PADDING_LEFT, 0, 0);

            LblTitle.Text = Translator.GetText("choose_lang");
            LblTitle.Font = UIFont.FromName("Arial", 16f);
            LblTitle.TextColor = Style.Header.TextColor;

            LblTitle.Frame = new CGRect((nfloat)LangTableRow.PADDING_LEFT, (MainTitle.Bounds.Height / 2) - (LblTitle.Bounds.Height / 2), UIScreen.MainScreen.Bounds.Width, TableView.Frame.Top);
            LblTitle.SizeToFit();

            // Initialize table
            //TableView.Frame = new CGRect(0, MainTitle.Frame.Y + MainTitle.Frame.Size.Height, UIScreen.MainScreen.Bounds.Width, 0);
            TableView.Source = new LangTableSource(ViewModel.Langs, this);
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            TableView.SeparatorColor = Style.OptionView.LineSeparatorColor;
            TableView.BackgroundColor = Style.OptionView.TableBackgroundColor;
            TableView.SeparatorInset = UIEdgeInsets.Zero;
            TableView.TintColor = Style.OptionView.ContentColor;

            _spinner = new Spinner(View);
        }

        public void UpdateLang(Core.Utilities.Lang lang)
        {
            TableView.UserInteractionEnabled = false;
            ViewModel.SelectedLang = lang.Value;
            _spinner.Display();

            ApiService.LoadMenuItems(OnMenuItemsLoaded);
        }

        private void OnMenuItemsLoaded(RootMenuApi result)
        {
            MenuViewModel.MenuItems = result.items;
            ViewModel.ShowHomeCommand.Execute();
        }
    }
}