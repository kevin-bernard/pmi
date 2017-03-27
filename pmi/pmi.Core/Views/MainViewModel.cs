using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using pmi.Core.Services;
namespace pmi.Core.Views
{
    public class MainViewModel : Base.BaseViewModel
    {
        public static string GetUrlCommand(string page) {
            return UrlManager.getURL(page);
        }

        public void ShowMenu()
        {
            ShowViewModel<Menu.MenuViewModel>();
            ShowViewModel<Content.ContentViewModel>();
        }
    }
}
