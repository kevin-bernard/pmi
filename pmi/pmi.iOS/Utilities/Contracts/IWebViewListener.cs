using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace pmi.iOS.Utilities.Contracts
{
    public interface IWebViewListener
    {
        void OnWebViewFinishLoading(UIWebView webview);
    }
}
