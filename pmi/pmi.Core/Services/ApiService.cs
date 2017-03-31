using pmi.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pmi.Core.Services;

namespace pmi.Core.Services
{
    public static class ApiService
    {
        public delegate void OnGetMenuItemRequestDone(RootMenuApi result);

        private static OnGetMenuItemRequestDone _callback;

        public static void LoadMenuItems(OnGetMenuItemRequestDone callback) {

            _callback = callback;

            JsonRequester<RootMenuApi>.Request(String.Format("{0}/{1}?lang={2}",
                                        Properties.Resources.ResourceManager.GetString("API_BASE_URL"),
                                        Properties.Resources.ResourceManager.GetString("API_URL"),
                                        LangManager.AppLang), OnRequestDone);
        }

        public static void OnRequestDone(RootMenuApi result) {
            _callback(result);
        }
    }
}
