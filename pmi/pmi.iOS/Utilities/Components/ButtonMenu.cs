using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using pmi.Core.Utilities;
using UIKit;

namespace pmi.iOS.Utilities.Components
{
    public class ButtonMenu : UIButton
    {
        public MenuItem MenuItem { get; private set; }

        public override bool Selected {
            get { return base.Selected; }
            set
            {
                if (value)
                {
                    TintColor = Style.Menu.MenuItemSelectedColor;
                    //BackgroundColor = UIColor.LightGray;
                }
                else
                {
                    TintColor = Style.Menu.MenuItemColor;
                   // BackgroundColor = UIColor.Clear;
                }

                base.Selected = value;
            }
        }
        public ButtonMenu(CGRect frame, MenuItem item, UIImage img) : base(frame)
        {
            MenuItem = item;

            SetTitle(item.menu_title, UIControlState.Normal);
            TintColor = Style.Menu.MenuItemColor;
            Font = UIFont.FromName("Arial", 14f);
            SetTitleColor(Style.Menu.MenuItemColor, UIControlState.Normal);
            SetTitleColor(Style.Menu.MenuItemSelectedColor, UIControlState.Selected);
            TitleEdgeInsets = new UIEdgeInsets(TitleEdgeInsets.Top, 10, TitleEdgeInsets.Bottom, TitleEdgeInsets.Right);
            HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            SetImage(img, UIControlState.Normal);
        }
    }
}