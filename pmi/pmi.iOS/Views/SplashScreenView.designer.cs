// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace pmi.iOS.Views
{
    [Register ("SplashScreenController")]
    partial class SplashScreenView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView Layout { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView MainTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Layout != null) {
                Layout.Dispose ();
                Layout = null;
            }

            if (MainTitle != null) {
                MainTitle.Dispose ();
                MainTitle = null;
            }
        }
    }
}