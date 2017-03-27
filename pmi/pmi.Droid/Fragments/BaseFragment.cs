using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V4.Widget;

namespace pmi.Droid.Fragments
{
    public abstract class BaseFragment : MvxFragment
    {
        protected Toolbar Toolbar { get; private set; }

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentId, null);
            
            var mainActivity = Activity as Activities.MainActivity;

            if (mainActivity == null) return view;

            Toolbar = mainActivity.FindViewById<Toolbar>(Resource.Id.toolbar);

            Toolbar.Title = string.Empty;

            mainActivity.SetSupportActionBar(Toolbar);
            
            if (true)
            {
                mainActivity.DrawerToggle = new MvxActionBarDrawerToggle(
                    Activity,
                    mainActivity.DrawerLayout,
                    Toolbar,
                    Resource.String.drawer_open, 
                    Resource.String.drawer_close
                );

                mainActivity.DrawerToggle.DrawerOpened += (sender, e) => mainActivity?.HideSoftKeyboard();
                
                mainActivity.DrawerLayout.AddDrawerListener(mainActivity.DrawerToggle);

                mainActivity.DisplayIconNavigateOnMenu();
                
                Toolbar.SetNavigationOnClickListener(new Utilities.Events.ToolbarClickListener(mainActivity.DrawerLayout, Toolbar, OnClickedMenu));
            }
            
            return view;

        }

        protected abstract int FragmentId { get; }
        
        public virtual void OnClickedMenu(Utilities.Events.ToolbarClickListener.CLICK_STATUS status) { }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}

