using System.Collections.Generic;
using System.Reflection;
using UIKit;

using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Support.SidePanels;
using MvvmCross.iOS.Support;
using MvvmCross.iOS.Support.JASidePanels;
using pmi.Core;

namespace pmi.iOS
{
	public class Setup : MvxIosSetup
	{
        private MvxApplicationDelegate _applicationDelegate;
        private UIWindow _window;

        /// <summary>Initializes a new instance of the <see cref="Setup"/> class.</summary>
        /// <param name="applicationDelegate">The application delegate.</param>
        /// <param name="window">The window.</param>
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
            _applicationDelegate = applicationDelegate;
            _window = window;
        }

	    protected override IEnumerable<Assembly> GetViewAssemblies()
	    {
            var list = new List<Assembly>();
            //list.AddRange(base.GetViewModelAssemblies());

	        list.Add(typeof(Acr.Settings.Settings).Assembly);
	        list.Add(typeof(Acr.DeviceInfo.DeviceInfo).Assembly);

            return base.GetViewAssemblies();
	    }

	    /// <summary>Creates the application.</summary>
        /// <returns>The IMvxApplication <see langword="object"/></returns>
        protected override IMvxApplication CreateApp()
        {
            return AppManager.CurrentApplication;
        }
        
        /// <summary>Creates the debug trace.</summary>
        /// <returns>The IMvxTrace <see langword="object"/></returns>
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
			return new MvxSidePanelsPresenter((MvxApplicationDelegate)ApplicationDelegate, Window);
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            //Mvx.RegisterSingleton<IDialogService>(() => new TouchDialogService());
            //register the presentation hint to pop to root
            //picked up in the third view model
            Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint(MvxPanelEnum.Center));
        }
	}
}
