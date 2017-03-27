using System.Reflection;
using System.Resources;

namespace pmi.Core.Services
{
    public static class Translator
    {
        private static ResourceManager _rm;
        
        public static string GetText(string key) {

            if (null == _rm) {
                _rm = GetResourceManager();
            }

            return _rm.GetString(key.Replace("-", "_"), new System.Globalization.CultureInfo(LangManager.DeviceLang));
        }

        private static ResourceManager GetResourceManager() {
            return new ResourceManager("pmi.TranslationResources", typeof(Translator).GetTypeInfo().Assembly); 
        }
    }
}
