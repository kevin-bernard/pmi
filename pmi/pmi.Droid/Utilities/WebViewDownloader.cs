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

            WebClient webClient = new WebClient();

            var path =
                Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).Path;

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    Toast.MakeText(context, String.Format("Fail to create directory at {0}", path), ToastLength.Long).Show();
                    return;
                }
            }

            string documentsPath = Path.Combine(path, String.Format("pmi_download_{0}.pdf", Directory.EnumerateFiles(path).Count()));

            webClient.DownloadFileCompleted += ((s, e) =>
            {
                var msg = String.Format("Done writing file at {0}", documentsPath);

                if (e.Error != null)
                {
                    msg = String.Format("Error writing file at {0}", documentsPath);
                }
                
                Toast.MakeText(context, msg, ToastLength.Long).Show();

            });

            webClient.DownloadFileAsync(new Uri(url), documentsPath);
        }
    }

}