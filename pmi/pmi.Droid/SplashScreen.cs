using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Views;
using pmi.Core.Services;
using pmi.Core.Utilities;
using System.Diagnostics;

namespace pmi.Droid
{
    [Activity(
        Label = "PMI", 
        MainLauncher = true, 
        Icon = "@drawable/icon",
        Theme = "@style/AppTheme",
        NoHistory = true, 
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.splash_screen)
        {
            
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            FindViewById<TextView>(Resource.Id.toolbar_title).Text = AppInfo.NAME;
        }
    }
}