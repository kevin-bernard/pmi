using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using System.Threading.Tasks;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Webkit;

using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

using pmi.Core.Views.Menu;
using pmi.Core.Views;

using pmi.Droid.Activities;
using pmi.Droid.Utilities;
using Calligraphy;
using pmi.Core.Utilities;
using Android.Support.V7.Widget;
using pmi.Droid.Utilities.Events;

namespace pmi.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("pmi.droid.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;

        private static List<MenuItem> _items = new List<MenuItem>();

        public static bool IsUrlContainedInMenu(string url)
        {

            foreach (MenuItem item in _items)
            {
                if (url == item.url)
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            ViewModel.TriggerSwitchUrl += OnSwitchUrl;
            ViewModel.TriggerLoadMenuItems += OnMenuItemsLoaded;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.fragment_navigation, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);

            return view;
        }
        
        public void OnSwitchUrl(string url) {
            Activity.FindViewById<WebView>(Resource.Id.webView).LoadUrl(url);
        }

        public void OnMenuItemsLoaded(List<MenuItem> items) {
            _items = items;
            
            for (int i = 0; i < items.Count; i++)
            {
                var menuItem = _navigationView.Menu.GetItem(i);

                if (menuItem != null)
                {
                    var item = _items[i];
                    menuItem.SetTitle(item.menu_title);
                }
            }

            ToolbarClickListener.Enabled = true;
            _navigationView.Menu.PerformIdentifierAction(Resource.Id.home, 0);
        }
        
        bool NavigationView.IOnNavigationItemSelectedListener.OnNavigationItemSelected(IMenuItem menuItem)
        {
            menuItem.SetCheckable(true);
            menuItem.SetChecked(true);
            
            var curItem = _items.Find(i => i.menu_title.Equals(menuItem.TitleFormatted.ToString()));

            if (curItem != null) {

                OnSwitchUrl(curItem.url);
                ((MainActivity)Activity).SetTitle(menuItem.TitleFormatted.ToString());
                ((MainActivity)Activity).DrawerLayout.CloseDrawers();
            }

            return true;
        }
        
        private IMenuItem GetItem(int id) {
            return _navigationView.Menu.FindItem(id);
        }
    }
}