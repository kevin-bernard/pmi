using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Views;
using pmi.Core.Services;
using pmi.Core.Utilities;
using System.Diagnostics;
using Android;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;

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

        public void CheckRequiredPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.WriteExternalStorage)
                        != Permission.Granted)
                {
                    RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 1);
                }
            }
        }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            FindViewById<TextView>(Resource.Id.toolbar_title).Text = AppInfo.NAME;

            CheckRequiredPermissions();
        }
    }
}