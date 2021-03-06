using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using pmi.iOS.Services;
using UIKit;

namespace pmi.iOS.Utilities
{
    public static class Style
    {
        public static void InjectMainBackground(this UIView layout, CGRect bounds)
        {
            var imgView = new UIImageView(UIImage.FromBundle("background"));
            imgView.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            layout.InsertSubview(imgView, 0);
        }

        public static void InjectBackground(this UIView layout, CGRect bounds, UIImage image)
        {
            var imgView = new UIImageView(image);

            imgView.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            layout.InsertSubview(imgView, 0);
        }

        public static void ApplyTitleStyle(this UITextView title)
        {
            title.BackgroundColor = Style.Header.BackgroundColor;
            title.TextColor = Style.Header.TextColor;
        }

        public static void AdaptSizeToScreen(this UIView view, int x = 0, int y = 0)
        {
            view.Frame = new CGRect(x, y, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
        }

        public static void AdaptSizeXToScreen(this UIView view, int x = 0, int y = 0)
        {
            view.Frame = new CGRect(x, y, UIScreen.MainScreen.Bounds.Width, view.Bounds.Height);
        }

        public static class Header
        {
            public static readonly UIColor BackgroundColor = UIColor.Clear.FromHexString("#1A516E");

            public static readonly UIColor TextColor = UIColor.White;
        }

        public static class Menu
        {
            public static readonly UIColor BackgroundColor = UIColor.Clear.FromHexString("#063251");
            public static readonly UIColor MenuItemColor = UIColor.White;
            public static readonly UIColor MenuItemSelectedColor = UIColor.Clear.FromHexString("#FEBB24");
        }

        public static class OptionView
        {
            public static readonly UIColor TableBackgroundColor = UIColor.Clear.FromHexString("#CDCFCF");

            public static readonly UIColor ContentColor = UIColor.Clear.FromHexString("#1D5878");

            public static readonly UIColor LineSeparatorColor = UIColor.Clear.FromHexString("#BABEBD");
        }

       
    }
}