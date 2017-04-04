using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using pmi.Core.Utilities;
using UIKit;
using pmi.iOS.Services;

namespace pmi.iOS.Utilities
{
    public class LangTableRow : UITableViewCell
    {
        UIImageView imageView;

        UITextView textLabel;

        public const double PADDING_LEFT = 60;
        
        public LangTableRow(NSString cellId) : base (UITableViewCellStyle.Value2, cellId)
        {

            textLabel = new UITextView()
            {
                Font = UIFont.FromName("Arial", 14f),
                TextColor = Style.OptionView.ContentColor,
                BackgroundColor = UIColor.Clear,
                Editable = false,
                Selectable = false
            };

            imageView = new UIImageView();

            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            
            ContentView.LayoutMargins = new UIEdgeInsets(0, 0, 0, 0);

            ContentView.Superview.BackgroundColor = UIColor.Clear;
            ContentView.Superview.TintColor = Style.OptionView.ContentColor;

            ContentView.TintColor = Style.OptionView.ContentColor;
            ContentView.AddSubviews(new UIView[] { imageView, textLabel });

        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            imageView.Frame = new CGRect(PADDING_LEFT, (ContentView.Bounds.Height / 2) - (imageView.Image.Size.Height / 2), imageView.Image.Size.Width, imageView.Image.Size.Height);
            textLabel.Frame = new CGRect(PADDING_LEFT + imageView.Frame.Width + 10, (ContentView.Bounds.Height / 2) - (textLabel.ContentSize.Height / 2), ContentView.Bounds.Width - 63, 25);
        }

        public void UpdateCell(string title, UIImage image)
        {
            imageView.Image = image;
            textLabel.Text = title;
        }
    }
}