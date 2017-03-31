using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Java.Lang.Reflect;

namespace pmi.Core.Services
{
    public static class WebRequester
    {
        public delegate void RequestDone(string response);
        
        public static async void GetResponse(string url, string contentType, string method, RequestDone callback)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

            request.ContentType = "application/json";
            request.Method = method;

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
