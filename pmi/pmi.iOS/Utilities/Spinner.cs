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

        public Spinner(UIView parent)
        {
            _parent = parent;

            var firstImage = UIImage.FromBundle("spinning_circle.png");

            var container = new UIView(new CGRect((parent.Frame.Width / 2) - (firstImage.Size.Width / 2), (parent.Frame.Height / 2) - (firstImage.Size.Height / 2), firstImage.Size.Width, firstImage.Size.Height));
           
            _imgView = new UIImageView(firstImage);
            _imgView.Hidden = true;

            CABasicAnimation rotationAnimation = CABasicAnimation.FromKeyPath("transform.rotation");
            rotationAnimation.To = NSNumber.FromDouble(Math.PI * 2); // full rotation (in radians)
            rotationAnimation.RepeatCount = int.MaxValue; // repeat forever
            rotationAnimation.Duration = 1;

            // Give the added animation a key for referencing it later (to remove, in this case).
            _imgView.Layer.AddAnimation(rotationAnimation, "rotationAnimation");

            container.AddSubview(_imgView);

            _parent.AddSubview(container);
        }


        public void Display()
        {
            _imgView.StartAnimating();
            _imgView.Hidden = false;
        }

        public void Hide()
        {
            _imgView.StopAnimating();
            _imgView.Hidden = true;
        }
    }
}