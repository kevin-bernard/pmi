using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace pmi.Core
{
    public static class AppManager
    {
        private static App app;
        
        public static App CurrentApplication {
            get {

                if (app == null) {
                    app = new App();
                }

                return app;
            }
        }
    }
}
