using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Core.ViewModels;
using Calligraphy;
using Android.Graphics;

namespace pmi.Droid.Activities
{
    public class BaseActivity<T> : MvxCachingFragmentCompatActivity<T> where T : class, IMvxViewModel
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
        }

        public void SetTitle(string title) {
            TitleView.Text = title; ;
        }

        public void SetTitleGravity(GravityFlags gravity) {
            TitleView.Gravity = gravity;
        }

        public void SetFont(TextView element, string fontName) {
            Typeface tf = Typeface.CreateFromAsset(Assets, string.Format("fonts/{0}", fontName));
            element.Typeface = tf;
        }
        
        protected TextView TitleView {
            get {
                return FindViewById<TextView>(Resource.Id.toolbar_title);
            }
        }

        protected void SetFonts() {
            SetFont(FindViewById<TextView>(Resource.Id.toolbar_title), "PTSans-Bold.ttf");
        }
    }
}