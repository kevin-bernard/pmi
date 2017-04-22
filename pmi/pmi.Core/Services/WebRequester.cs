using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace pmi.Core.Services
{
    public static class WebRequester
    {
        public delegate void RequestDone(string response);
        
        public static void GetResponse(string url, string contentType, string method, RequestDone callback)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

            request.ContentType = contentType;
            request.Method = method;

            try
            {
                SendRequest(request, callback);
            }
            catch
            {
                AppManager.CurrentApplication.Error = new Exception(Translator.GetText("error_no_internet"));

                callback(string.Empty);
            }

        }

        private static void SendRequest(HttpWebRequest request, RequestDone callback)
        {
            var responseTask = Task.Factory.FromAsync<WebResponse>
                                                  (request.BeginGetResponse,
                                                   request.EndGetResponse,
                                                   null);
           
            responseTask.ContinueWith(t => ReadStreamFromResponse(t.Result, callback));
            responseTask.Wait(3000);
        }

        private static void ReadStreamFromResponse(WebResponse response, RequestDone callback)
        {
            
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                string content = streamReader.ReadToEnd();

                callback(content);
            }
        }

    }
}
