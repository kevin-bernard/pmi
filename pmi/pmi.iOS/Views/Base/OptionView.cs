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

namespace pmi.iOS.Views
{
    [Register("OptionView")]
	[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class OptionView : BaseViewController<OptionViewModel>
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UITableView TableView { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UITextView MainTitle { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        public UIView Layout { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MainTitle.Text = Translator.GetText("choose_lang");
            
            MainTitle.TextContainer.LineFragmentPadding = 0;
            MainTitle.ApplyTitleStyle();

            MainTitle.TextContainerInset = new UIEdgeInsets((MainTitle.Bounds.Height / 2) - (MainTitle.ContentSize.Height / 2), (nfloat)LangTableRow.PADDING_LEFT, 0, 0);
            
            Layout.InjectMainBackground(this.View.Bounds);
            Layout.AdaptSizeToScreen();
            
            // Initialize table
            TableView.Source = new LangTableSource(ViewModel.Langs, this);
            TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            TableView.SeparatorColor = Style.OptionView.LineSeparatorColor;
            TableView.BackgroundColor = Style.OptionView.TableBackgroundColor;
            TableView.SeparatorInset = UIEdgeInsets.Zero;
            TableView.TintColor = Style.OptionView.ContentColor;
        }

        public void UpdateLang(Lang lang)
        {
            ViewModel.SelectedLang = lang.Value;

            ApiService.LoadMenuItems(OnMenuItemsLoaded);

        }

        private void OnMenuItemsLoaded(RootMenuApi result)
        {
            MenuViewModel.MenuItems = result.items;
            ViewModel.ShowHomeCommand.Execute();
        }
    }   
}