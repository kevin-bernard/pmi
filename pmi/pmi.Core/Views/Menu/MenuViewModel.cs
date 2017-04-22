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

        public static List<MenuItem> MenuItems { get; set; }

        public static bool IsUrlContainedInMenu(string url)
        {
            return GetItemFromUrl(url) != null || url.ToLower().Contains("conference") == false;
        }

        public static MenuItem GetItemFromUrl(string url)
        {
            MenuItems = MenuItems ?? new List<MenuItem>();

            foreach (MenuItem item in MenuItems)
            {
                if (url == item.url)
                {
                    return item;
                }
            }

            return null;
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
