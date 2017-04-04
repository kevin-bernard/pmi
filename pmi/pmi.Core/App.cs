using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using pmi.Core.Services;
using System;
using Xamarin.Forms;

namespace pmi.Core
{
    public class App : MvxApplication
    {
        public Exception Error;

        public override void Initialize()
        {
            base.Initialize();

            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            
            Mvx.RegisterSingleton(Acr.Settings.Settings.Local);

            // request a reference to the constructed appstart object 
            var appStart = Mvx.Resolve<IMvxAppStart>();
            
            // register the appstart object
            RegisterAppStart(appStart);
        }
    }
}