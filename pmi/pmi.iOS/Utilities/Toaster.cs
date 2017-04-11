using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace pmi.iOS.Utilities
{
    public static class Toaster
    {
        public static void Make(UIView parent, string text, double duration = 3.0)
        {
            var toast = new Toast();
            toast.Display(parent, text, duration);
        }
    }
}