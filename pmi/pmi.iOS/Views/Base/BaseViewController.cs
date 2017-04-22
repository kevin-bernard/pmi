using System;
using UIKit;
using pmi.Core.Views.Base;
using MvvmCross.iOS.Views;

namespace pmi.iOS.Views
{
    /// <summary>
    /// A base view controller 
    /// </summary>
    public class BaseViewController<TViewModel> : MvxViewController where TViewModel : BaseViewModel
    {
        #region Fields

        protected bool NavigationBarEnabled = false;
        
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        #endregion

        #region Public Methods

        public BaseViewController()
        {

        }

        public BaseViewController(IntPtr arg) : base(arg)
        {

        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public override void ViewDidLoad()
        {
            EdgesForExtendedLayout = UIRectEdge.None;
            View.BackgroundColor = UIColor.White;
            UIApplication.SharedApplication.SetStatusBarHidden(true, true);
            NavigationController.NavigationBar.TintColor = UIColor.Clear;
            UIApplication.SharedApplication.StatusBarHidden = true;
            //NavigationController.SetNavigationBarHidden(true, true);
            //NavigationController.SetToolbarHidden(true, true);

            base.ViewDidLoad();
        }
        
        #endregion
    }
}