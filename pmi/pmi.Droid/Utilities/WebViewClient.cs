using Android.Animation;
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

            _prgBar.Visibility = ViewStates.Invisible;
        }

        public override void OnPageStarted(WebView view, string url, Bitmap favicon)
        {
            _prgBar.Visibility = ViewStates.Visible;

            //view.Animate().Alpha(1).SetDuration(1000).WithEndAction(new Runnable(() => {
            //    view.Alpha = 0;
            //}));

            AnimateTransition(view, true);

            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);

            _callbackDone(url);

            _prgBar.Visibility = ViewStates.Invisible;
            
            AnimateTransition(view);
        }

        public void AnimateTransition(WebView view, bool hideIt = false) {

            view.HasTransientState = true;

            float start = 0, end = 1;

            if (hideIt)
            {
                start = 1;
                end = 0;
            }

            ValueAnimator animator = ValueAnimator.OfFloat(new[] { start, end });

            animator.SetDuration(1000);

            animator.Update += (o, animatorUpdateEventArgs) => {
                view.Alpha = (float)animatorUpdateEventArgs.Animation.AnimatedValue;
            };

            animator.AnimationEnd += delegate {
                view.Alpha = end;
            };

            animator.Start();
        }
    }
}