using Android.Graphics;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Lang;

namespace pmi.Droid.Utilities
{
    public class WebViewClient : Android.Webkit.WebViewClient
    {
        public delegate void OnPageEvent(string url);

        private OnPageEvent _callbackDone;

        private ProgressBar _prgBar;

        public WebViewClient(ProgressBar prgBar, OnPageEvent callbackDone) {
            _callbackDone = callbackDone;
            _prgBar = prgBar;
        }

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            _prgBar.Visibility = ViewStates.Visible;


            view.Animate().SetDuration(1000).WithEndAction(new Runnable(() => {
                view.Alpha = 0;
            }));

            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);

            _callbackDone(url);

            _prgBar.Visibility = ViewStates.Invisible;

            view.Animate().SetDuration(1000).WithEndAction(new Runnable(() => {
                view.Alpha = 1f;
            }));
        }
    }
}