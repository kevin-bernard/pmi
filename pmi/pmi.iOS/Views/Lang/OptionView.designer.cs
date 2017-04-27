// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace pmi.iOS.Views.Lang
{
    [Register ("OptionView")]
    partial class OptionView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView MainTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView TableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LblTitle != null) {
                LblTitle.Dispose ();
                LblTitle = null;
            }

            if (MainTitle != null) {
                MainTitle.Dispose ();
                MainTitle = null;
            }

            if (TableView != null) {
                TableView.Dispose ();
                TableView = null;
            }
        }
    }
}