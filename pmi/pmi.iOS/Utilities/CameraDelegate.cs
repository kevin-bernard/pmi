using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace pmi.iOS.Utilities
{

    public class CameraDelegate : UIImagePickerControllerDelegate
    {
        Action action { get; set; }

        public CameraDelegate(Action e)
        {
            action = e;
        }

        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            picker.DismissModalViewController(true);
            var image = info.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;

            action.DynamicInvoke(image);

        }
    }
}