using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace pmi.iOS.Utilities
{
    public class Spinner
    {
        private UIView _parent;

        private UIImageView _imgView;

        private UIActivityIndicatorView activitySpinner;

        public Spinner(UIView parent)
        {
            _parent = parent;

            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);

            var firstImage = UIImage.FromBundle("spinning_circle.png");

            activitySpinner.Frame = new CGRect((parent.Frame.Width / 2) - (activitySpinner.Frame.Width / 2), (parent.Frame.Height / 2) - (activitySpinner.Frame.Height / 2), activitySpinner.Frame.Width, activitySpinner.Frame.Height);
            activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
            activitySpinner.Hidden = true;
            activitySpinner.Color = UIColor.Black;
            _parent.AddSubview(activitySpinner);

        }


        public void Display()
        {
            activitySpinner.StartAnimating();
            activitySpinner.Hidden = false;
        }

        public void Hide()
        {
            activitySpinner.StopAnimating();
            activitySpinner.Hidden = true;
        }

        public Spinner Refresh()
        {
            _imgView.Layer.RemoveAllAnimations();
            AddAnimation();

            return this;
        }

        private void AddAnimation()
        {
            CABasicAnimation rotationAnimation = CABasicAnimation.FromKeyPath("transform.rotation");
            rotationAnimation.To = NSNumber.FromDouble(Math.PI * 2); // full rotation (in radians)
            rotationAnimation.RepeatCount = int.MaxValue; // repeat forever
            rotationAnimation.Duration = 0.5;

            // Give the added animation a key for referencing it later (to remove, in this case).
            _imgView.Layer.AddAnimation(rotationAnimation, "rotationAnimation");
        }
    }
}