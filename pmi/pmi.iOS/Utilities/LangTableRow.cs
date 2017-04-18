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

        public delegate void OnItemClick(NSIndexPath indexPath);

        private static int count = 1;

        private OnItemClick onClickCallback;

        public LangTableRow(NSString cellId, OnItemClick itemClick) : base (UITableViewCellStyle.Value2, cellId)
        {
            onClickCallback = itemClick;

            textLabel = new UITextView()
            {
                Font = UIFont.FromName("Arial", 14f),
                TextColor = Style.OptionView.ContentColor,
                BackgroundColor = UIColor.Clear,
                Editable = false,
                Selectable = false,
                UserInteractionEnabled = true,
                TextContainerInset = UIEdgeInsets.Zero
            };

            imageView = new UIImageView();

            SelectionStyle = UITableViewCellSelectionStyle.Gray;
            
            ContentView.LayoutMargins = new UIEdgeInsets(0, 0, 0, 0);

            ContentView.Superview.BackgroundColor = UIColor.Clear;
            ContentView.Superview.TintColor = Style.OptionView.ContentColor;

            ContentView.TintColor = Style.OptionView.ContentColor;
            ContentView.AddSubviews(new UIView[] { imageView, textLabel });

            if (count++ % 2 == 0)
            {
                //ContentView.BackgroundColor = UIColor.Black;
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            imageView.Frame = new CGRect(PADDING_LEFT, (ContentView.Bounds.Height / 2) - (imageView.Image.Size.Height / 2), imageView.Image.Size.Width, imageView.Image.Size.Height);

            textLabel.Frame = new CGRect(PADDING_LEFT + imageView.Frame.Width + 10, (ContentView.Bounds.Height / 2) - (+textLabel.ContentSize.Height / 2), ContentView.Frame.Width, ContentView.Frame.Size.Height);

            textLabel.SizeToFit();
        }

        public void UpdateCell(string title, UIImage image, NSIndexPath indexPath)
        {
            imageView.Image = image;
            textLabel.Text = title;

            UITapGestureRecognizer tgrLabel = new UITapGestureRecognizer(() => {
                Selected = true;
                onClickCallback(indexPath);
            });

            textLabel.AddGestureRecognizer(tgrLabel);
        }
    }
}