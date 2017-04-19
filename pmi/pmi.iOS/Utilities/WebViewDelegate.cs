using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Foundation;
using pmi.Core.Views.Menu;
using UIKit;
using Xamarin.Forms;
using pmi.iOS.Utilities.Contracts;

namespace pmi.iOS.Utilities
{
    public class WebViewDelegate : UIWebViewDelegate, IUIWebViewDelegate
    {
        private UIView _parent;

        private IWebViewListener _listener;

        private Spinner _spinner;

        public WebViewDelegate(UIView parent, IWebViewListener listener)
        {
            _parent = parent;
            _listener = listener;
            _spinner = new Spinner(parent);
        }

        public override void LoadStarted(UIWebView webView)
        {
            LoadStartAnimate(webView);
        }

        public override void LoadingFinished(UIWebView webView)
        {
            
            _listener.OnWebViewFinishLoading(webView);

            LoadFinishAnimate(webView);
        }

        public override bool ShouldStartLoad(UIWebView webView, NSUrlRequest request, UIWebViewNavigationType navigationType)
        {
           
            if (request.Url.IsFileUrl || (request.Url.Query != null && request.Url.Query.ToLower().Contains("pdf")))
            {
                try
                {
                    LoadStartAnimate(webView);

                    DownloadFile(webView, request, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                }
                catch(Exception e)
                {
                    Toaster.Make(_parent, e.Message);
                }


                return false;
            }
        
            return true;
        }
        
        private void DownloadFile(UIWebView webview, NSUrlRequest request, string documentsPath)
        {
            var webClient = new WebClient();

            string localFilename = "pmi_download.pdf";
            string localPath = Path.Combine(documentsPath, localFilename);

            webClient.DownloadFileCompleted += (s, e) => {

                if (e.Error == null)
                {
                    Toaster.Make(_parent, String.Format("Done writing file in MyDocuments")); 
                }
                else
                {
                    Toaster.Make(_parent, e.Error.Message);
                }

                LoadFinishAnimate(webview);
            };
            
            webClient.DownloadFileAsync(new Uri(request.Url.ToString()), localPath);
        }

        public void AnimateTransition(UIWebView view, bool hideIt = false)
        {

            float start = 0, end = 1;

            if (hideIt)
            {
                start = 1;
                end = 0;
            }

            UIView.Animate(0.4, () =>
            {
                view.Alpha = end;
            });
        }

        private void LoadStartAnimate(UIWebView webView)
        {
            AnimateTransition(webView, true);
            _spinner.Display();
        }

        private void LoadFinishAnimate(UIWebView webView)
        {
            AnimateTransition(webView, false);
            _spinner.Hide();
        }
    }
}