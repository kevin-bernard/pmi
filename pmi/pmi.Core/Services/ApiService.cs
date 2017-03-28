using pmi.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static pmi.Core.Services.JsonRequester;

namespace pmi.Core.Services
{
    public static class ApiService
    {
        public static void LoadMenuItems(RequestDone callback) {
            JsonRequester.Request(String.Format("{0}/{1}?lang={2}",
                                        Properties.Resources.ResourceManager.GetString("API_BASE_URL"),
                                        Properties.Resources.ResourceManager.GetString("API_URL"),
                                        LangManager.AppLang), callback);
        }
    }
}
