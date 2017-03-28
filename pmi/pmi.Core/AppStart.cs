
using MvvmCross.Core.ViewModels;
using System;

using pmi.Core.Services;
using System.Diagnostics;
using pmi.Core.Utilities;

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
            //LangManager.AppLang = null;

            if (LangManager.HasAppLang || !LangManager.DeviceHasManySubLanguages)
            {
                LangManager.InitAppLang();
                ApiService.LoadMenuItems(OnRequestDone);
            }
            else {
                StartApp();
            }
        }

        private void StartApp() {
            if (LangManager.AppLang == null || LangManager.AppLang == string.Empty)
            {
                if (LangManager.DeviceHasManySubLanguages)
                {
                    ShowViewModel<Views.OptionViewModel>();
                }
                else
                {
                    ShowMainView();
                }
            }
            else
            {
                ShowMainView();
            }
        }

        private void ShowMainView()
        {
            ShowViewModel<Views.MainViewModel>();
        }

        private void OnRequestDone(RootMenuApi result) {
            Views.Menu.MenuViewModel.MenuItems = result.items;
            StartApp();
        }
    }
}