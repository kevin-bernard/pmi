using System;

using MvvmCross.Core.ViewModels;

using pmi.Core.Views.Base;
using pmi.Core.Services;
using pmi.Core.Utilities;
using System.Collections.Generic;

namespace pmi.Core.Views.Menu
{
    public class MenuViewModel : BaseViewModel
    {
        public delegate void OnSwitchUrl(string url);

        public event OnSwitchUrl TriggerSwitchUrl;

        public delegate void OnMenuItemsLoaded(List<MenuItem> items);

        public event OnMenuItemsLoaded TriggerLoadMenuItems;

        public bool MenuItemsLoaded => false;

        public MenuViewModel() {
            JsonRequester.Request(String.Format("{0}/{1}?lang={2}",
                                        Properties.Resources.ResourceManager.GetString("API_BASE_URL"),
                                        Properties.Resources.ResourceManager.GetString("API_URL"),
                                        LangManager.AppLang), OnMenuItemsRequestDone);
        }

        public void PerformClick(string item) {
            SwitchUrlAction(item.ToUpper());
        }

        public void SwitchUrlAction(string page)
        {
            TriggerSwitchUrl(UrlManager.getURL(page));
        }
        
        private void OnMenuItemsRequestDone(RootMenuApi result) {
            TriggerLoadMenuItems(result.items);
        }
    }
}
