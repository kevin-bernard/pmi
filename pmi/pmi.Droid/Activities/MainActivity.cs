using Android.App;
using Android.OS;
using pmi.Core.Views;

using MvvmCross.Droid.Support.V7.AppCompat;
using Android.Support.V4.Widget;
using Android.Views.InputMethods;
using Android.Views;
using Android.Support.V4.View;
using Android.Content.PM;
using Android.Webkit;
using Android.Support.V7.Widget;
using pmi.Droid.Utilities.Events;
using Android.Support.Design.Widget;
using pmi.Core.Views.Menu;
using pmi.Droid.Fragments;

namespace pmi.Droid.Activities
{
    [Activity (
        Label = "PMI", 
        Theme = "@style/AppTheme",
        Icon = "@drawable/icon",
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.Multiple,
        Name = "pmi.droid.activities.MainActivity"
    )]
    public class MainActivity : BaseActivity<MainViewModel>
    {

        public DrawerLayout DrawerLayout;

        public MvxActionBarDrawerToggle DrawerToggle { get; set; }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
                ViewModel.ShowMenu();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
                DrawerLayout.CloseDrawers();
            else
                base.OnBackPressed();
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null) return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            var webview = FindViewById<WebView>(Resource.Id.webView);

            if ((keyCode == Keycode.Back) && webview != null && webview.CanGoBack())
            {
                webview.GoBack();

                var item = MenuViewModel.GetItemFromUrl(webview.OriginalUrl);

                if (item != null) {
                    SetTitle(item.menu_title);
                }

                return true;
            }

            Finish();
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

            return true;
            //return base.OnKeyDown(keyCode, e);
        }

        public void DisplayBackArrowOnMenu() {

            DrawerToggle.DrawerIndicatorEnabled = false;
            DrawerToggle.OnDrawerStateChanged(DrawerLayout.LockModeLockedClosed);
            DrawerToggle.SyncState();
            
            SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar?.SetDisplayShowHomeEnabled(true);
            
            ToolbarClickListener.MustGoBack = true;
        }

        public void DisplayIconNavigateOnMenu() {
            
            DrawerToggle.OnDrawerStateChanged(DrawerLayout.LockModeUnlocked);
            DrawerToggle.DrawerIndicatorEnabled = true;

            SupportActionBar?.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar?.SetDisplayShowHomeEnabled(false);

            DrawerToggle.SyncState();

            ToolbarClickListener.MustGoBack = false;
        }
    }
}


