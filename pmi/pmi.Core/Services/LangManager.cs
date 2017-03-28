using System;
using System.Collections.Generic;
using System.Globalization;

using Foundation;

namespace pmi.Core.Services
{
    public static class LangManager
    {
        public static void InitAppLang() {

            var deviceLangs = DeviceAvailableLangs;

            if (deviceLangs.Length == 1)
            {
                AppLang = deviceLangs[0];
            }
            else
            {
                AppLang = DefaultLang;
            }
        }

        public static string DefaultLang {
            get {
                return Properties.Resources.ResourceManager.GetString("DEFAULT_LANG");
            }
        }

        public static bool HasAppLang {
            get {
                return AppLang != null && AppLang != string.Empty;
            }
        }
        public static bool DeviceHasManySubLanguages {
            get {
                string[] relatedLangs = DeviceAvailableLangs;

                return relatedLangs.Length > 1;
            }
        }

        public static string DeviceLang {
            get {
                return Acr.DeviceInfo.DeviceInfo.App.CurrentCulture.TwoLetterISOLanguageName;
            }
        }

        public static string BaseDeviceLang {
            get {
                return BaseLang(DeviceLang);
            }
        }

        public static string AppLang {
            get {
                return Acr.Settings.Settings.Local.Get<string>("LANG");
            }
            set
            {
                if (value is string) {
                    value = value.ToLower();
                }

                Acr.Settings.Settings.Local.Set<string>("LANG", value);
               
            }
        }
        
        public static string[] DeviceAvailableLangs {
            get {
                try
                {
                    return Properties.Resources.ResourceManager.GetString(DeviceLang.ToUpper()).Split(new char[1] { ',' });
                }
                catch
                { 
                    return new string[0];
                }
            }
        }

        public static string[] DeviceAvailableLangsTranslated {
            get {
                string[] langs = DeviceAvailableLangs;
                string[] translatedLangs = new string[langs.Length];

                int cursor=0;
                foreach (string lang in langs) {
                    translatedLangs[cursor++] = Translator.GetText(lang);
                }

                return translatedLangs;
            }
        }

        public static bool IsLangAvailable(string curLang) {

            string[] availableLanguages = DeviceAvailableLangs;

            curLang = curLang == null ? string.Empty : curLang.ToLower();

            foreach (string lang in availableLanguages)
            {
                if (lang.ToLower() == curLang)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static string BaseLang(string lang) {
            var splittedLang = lang.Split('-');

            return splittedLang.Length > 0 ? splittedLang[0] : String.Empty;
        }
    }
}
