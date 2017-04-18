
using MvvmCross.Core.ViewModels;
using System;

using pmi.Core.Services;
using System.Diagnostics;
using System.Linq;
using MvvmCross.Platform;
using pmi.Core.Utilities;
using pmi.Core.Views;
using pmi.Core.Views.Content;

namespace pmi.Core
{
    /// <summary>
    /// A class that implements the IMvxAppStart interface and can be used to customise
    /// the way an application is initialised
    /// </summary>
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// Start is called on startup of the app
        /// Hint contains information in case the app is started with extra parameters
        /// </summary>
        public void Start(object hint = null)
        {
            LangManager.AppLang = null;

            if (LangManager.HasAppLang)
            {
                ApiService.LoadMenuItems(OnLoadMenuItemsRequestDone);
            }
            else {
                StartApp();
            }
        }

        private void StartApp() {

            if (LangManager.HasAppLang)
            {
                ShowMainView();
            }
            else if (LangManager.DeviceHasManySubLanguages)
            {
                ShowViewModel<Views.OptionViewModel>();
            }
            else
            {
                LangManager.InitAppLang();
                ApiService.LoadMenuItems(OnLoadMenuItemsRequestDone);
            }
        }

        private void ShowMainView()
        {
            ShowViewModel<Views.MainViewModel>(true);
        }

        private void OnLoadMenuItemsRequestDone(RootMenuApi result) {
            Views.Menu.MenuViewModel.MenuItems = result.items;

            StartApp();
        }
    }
}