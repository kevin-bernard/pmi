using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace pmi.Core.Services
{
    static class UrlManager
    {
        public static string getURL(string page) {

            var rm = new ResourceManager(typeof(Properties.Resources));

            string endURL = rm.GetString(page);

            return Properties.Resources.BASE_URL + LangManager.AppLang + "/" + endURL;
        }  
    }
}
