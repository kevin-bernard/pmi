using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pmi.Core.Services;
using pmi.Core.Utilities;

namespace pmi.Core.Views
{
    public class OptionViewModel : Base.BaseViewModel
    {
        public string SelectedLang { get; set; }

        public string DeviceLang {
            get {
                return LangManager.DeviceLang;
            }
        }

        public List<Lang> Langs
        {
            get
            {
                var translations = LangManager.DeviceAvailableLangsTranslated;
                var values = LangManager.DeviceAvailableLangs;

                List<Lang> langs = new List<Lang>();

                for (int i = 0; i < translations.Length; i++)
                {
                    langs.Add(new Lang(translations[i], values[i]));
                }

                return langs;
            }
        }
        
        public virtual IMvxCommand ShowHomeCommand
        {
            get { return new MvxCommand(ShowHomeExecuted); }
        }

        public void ShowHomeExecuted()
        {
            LangManager.AppLang = SelectedLang;

            ShowViewModel<MainViewModel>();
        }
    }
}
