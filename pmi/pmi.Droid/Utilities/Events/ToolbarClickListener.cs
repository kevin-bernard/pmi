using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Support.V4.Content;

namespace pmi.Droid.Utilities.Events
{
    public class ToolbarClickListener : Java.Lang.Object, View.IOnClickListener
    {
        public enum CLICK_STATUS {
            GO_BACK=1,
            OPEN_MENU=2
        };

        public static bool Enabled { get; set; }

        public static bool MustGoBack { get; set; }

        public delegate void OnClicked(CLICK_STATUS status);

        private Android.Support.V4.Widget.DrawerLayout _layout;

        private Toolbar _toolbar;

        private OnClicked _callback;

        public ToolbarClickListener(Android.Support.V4.Widget.DrawerLayout layout, Toolbar toolbar, OnClicked callback) {

            _layout = layout;
            _toolbar = toolbar;
            _callback = callback;

            MustGoBack = false;
            Enabled = false;
        }

        public void OnClick(View v)
        {
            CLICK_STATUS status = MustGoBack ? CLICK_STATUS.GO_BACK : CLICK_STATUS.OPEN_MENU;

            //if (Enabled) {
            if (MustGoBack)
            {
                _layout.CloseDrawers();
            }
            else
            {
                _layout.OpenDrawer(GravityCompat.Start);
            }

            _callback(status);
            //}
        }
    }
}