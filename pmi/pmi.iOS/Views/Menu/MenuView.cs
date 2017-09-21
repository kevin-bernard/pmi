using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;
using Foundation;
using UIKit;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.iOS.Support.SidePanels;
using pmi.Core.Utilities;
using pmi.Core.Views.Menu;
using pmi.iOS.Utilities;
using pmi.iOS.Utilities.Components;
using pmi.Core.Services;

namespace pmi.iOS.Views
{
    [MvxPanelPresentation(MvxPanelEnum.Left, MvxPanelHintType.ActivePanel, false)]
    public partial class MenuView : BaseViewController<MenuViewModel>
    {
        private List<string> menuIds = new List<string>()
        {
            "home",
            "program",
            "conference",
            "map",
            "info",
            "gallery"
        };

        private ButtonMenu lastSelectedButton;

        public MenuView()
        {
        }

        public MenuView(IntPtr arg) : base(arg)
        {
        }
        public override void ViewDidLoad()
        {
            //base.ViewDidLoad();

            var scrollView = new UIScrollView(View.Frame)
            {
                ShowsHorizontalScrollIndicator = false,
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight,
                BackgroundColor = Style.Menu.BackgroundColor
            };

            int top = 120, left = 30;
            UIImage divider = UIImage.FromBundle("divider");

            MenuViewModel.MenuItems.Add(new MenuItem()
            {
                menu_title = Translator.GetText("gallery"),
                page_title = Translator.GetText("gallery"),
                url = string.Empty
            });

            for (var i = 0; i < MenuViewModel.MenuItems.Count; i++)
            {
                MenuItem item = MenuViewModel.MenuItems[i];

                if (MvxEnumerableExtensions.ElementAt(menuIds, i) != null)
                {
                    var img = UIImage.FromBundle(menuIds[i])?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                    var btnHeight = img?.Size.Height + 10 ?? 20;

                    var btn = new ButtonMenu(new CGRect(0, top, scrollView.Frame.Width, btnHeight), item, img)
                    {
                        ContentEdgeInsets = new UIEdgeInsets(0, left, 0, 0),
                        ContentMode = UIViewContentMode.ScaleAspectFit
                    };
                    
                    btn.TouchUpInside += OnMenuItemClick;

                    if (i == 0)
                    {
                        btn.Selected = true;
                        lastSelectedButton = btn;
                    }

                    scrollView.Add(btn);

                    top += (int)btn.Frame.Height;

                    var leftSeparator = left + btn.TitleEdgeInsets.Left;
                    leftSeparator += btn.CurrentImage?.Size.Width ?? 0;
                    
                    var separator = new UIImageView(new CGRect(leftSeparator, top, scrollView.Frame.Width - 160, 5))
                    {
                        Image = divider
                    };

                    scrollView.Add(separator);

                    top += (int)separator.Frame.Height + 10;
                }
                
            }

            Add(scrollView);
            

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                scrollView.AtLeftOf(View),
                scrollView.AtTopOf(View),
                scrollView.WithSameWidth(View),
                scrollView.WithSameHeight(View));

            //scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            // var constraints = scrollView.VerticalStackPanelConstraints(new Margins(20, 120, 0, 10, 5, 5), scrollView.Subviews);
            //
            // scrollView.AddConstraints(constraints);
        }

        public override void ViewWillAppear(bool animated)
        {
            Title = "Left Menu View";
            base.ViewWillAppear(animated);

            NavigationController.NavigationBarHidden = true;
        }

        public void OnMenuItemClick(object sender, EventArgs e)
        {
            ButtonMenu btn = (ButtonMenu) sender;

            btn.Selected = true;

            if (lastSelectedButton != null)
            {
                lastSelectedButton.Selected = false;
            }

            lastSelectedButton = btn;

            int index = 0;

            for (var i = 0; i < MenuViewModel.MenuItems.Count; i++)
            {
                if (MenuViewModel.MenuItems[i].page_title == btn.MenuItem.page_title) {
                    index = i;
                }
            }

            MenuClickEvent.Notify(btn.MenuItem.menu_title, btn.MenuItem.url, index);
        }
    }
}
