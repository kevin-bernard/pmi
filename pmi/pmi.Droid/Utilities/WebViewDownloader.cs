using System.Net;
using System.IO;
using System.Text;
using System;
using System.Linq;
using System.Security.AccessControl;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using pmi.Droid.Activities;

namespace pmi.Droid.Utilities
{
    public class WebViewDownloader : Java.Lang.Object, IDownloadListener
    {
        private Context context;

        public WebViewDownloader(Context ctx)
        {
            context = ctx;
        }

        public void OnDownloadStart(String url, String userAgent,
                    String contentDisposition, String mimetype,
                    long contentLength)
        {

            try
            {
                StartDownload(url);
            }
            catch (Exception e)
            {
                Toast.MakeText(context, e.Message, ToastLength.Long).Show();
            }
        }

        private void StartDownload(String url)
        {
            Download(url, Android.OS.Environment.DirectoryDownloads);
        }

        private void Download(string url, string dir)
        {
            var downloadManager = (DownloadManager)context.GetSystemService(Context.DownloadService);
            string filename = "pmi_download.pdf";

            context.RegisterReceiver(new Receiver(Path.Combine(dir, filename)), new IntentFilter(DownloadManager.ActionDownloadComplete));

            DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse(url));
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, filename);
            request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted); // to notify when download is complete
            
            downloadManager.Enqueue(request);
        }
    }

    class Receiver : BroadcastReceiver
    {
        private string _file;

        public Receiver(string file)
        {
            _file = file;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var msg = String.Format("Done writing file at {0}", _file);
            Toast.MakeText(context, msg, ToastLength.Long).Show();
        }
    }

}