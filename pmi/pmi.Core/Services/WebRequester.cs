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
            catch (Exception e)
            {
                AppManager.CurrentApplication.Error = e;

                callback(string.Empty);
            }

        }

        private static async void SendRequest(HttpWebRequest request, RequestDone callback)
        {
            var responseTask = Task.Factory.FromAsync<WebResponse>
                                                  (request.BeginGetResponse,
                                                   request.EndGetResponse,
                                                   null);

            using (var response = (HttpWebResponse)await responseTask)
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string content = streamReader.ReadToEnd();

                    callback(content);
                }
            }
        }

    }
}
