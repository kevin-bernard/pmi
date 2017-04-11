using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;

namespace pmi.iOS.Utilities
{
    public class Toast : UIView
    {
        private static double hLabelGap = 40.0;
        private static double vLabelGap = 20.0;
        private static double hToastGap = 20.0;
        private static double vToastGap = 10.0;

        private UILabel textLabel;

        public async void Display(UIView parent, string text, double duration)
        {
            var labelFrame = new CGRect(
                parent.Frame.X + hLabelGap,
                parent.Frame.Height - vLabelGap,
                parent.Frame.Width - 2 * hLabelGap,
                parent.Frame.Height - 2 * vLabelGap
            );

            textLabel = new UILabel()
            {
                Font = UIFont.FromName("Arial", 14f),
                Text = text,
                BackgroundColor = UIColor.Clear,
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.White,
                Frame = labelFrame
            };

            textLabel.SizeToFit();

            AddSubview(textLabel);

            Frame = new CGRect(
                textLabel.Frame.X - hToastGap,
                textLabel.Frame.Y - vToastGap,
                textLabel.Frame.Width + 2 * hToastGap,
                textLabel.Frame.Height + 2 * vToastGap
            );

            BackgroundColor = UIColor.DarkGray;
            Alpha = 0;

            Layer.CornerRadius = 20;

            Center = parent.Center;
            textLabel.Center = new CGPoint(Frame.Size.Width / 2, Frame.Size.Height / 2);

            parent.AddSubview(this);

            UIView.Animate(0.4, () =>
            {
                Alpha = (nfloat)0.9;
                textLabel.Alpha = (nfloat)0.9;
            });
            
            await DelayActionAsync(4000, Hide);
        }

        public async Task DelayActionAsync(int delay, Action action)
        {
            await Task.Delay(delay);

            action();
        }

        private void Hide()
        {
            UIView.Animate(0.4, () =>
            {
                Alpha = 0;
                textLabel.Alpha = 0;
            });
        }
    }
}