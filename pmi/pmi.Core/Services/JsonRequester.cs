using pmi.Core.Utilities;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace pmi.Core.Services
{
    public static class JsonRequester
    {
        public static string Method => "GET";

        public delegate void RequestDone(RootMenuApi result);

        private static RequestDone _callback;

        public static async void Request(string url, RequestDone callback)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

            request.ContentType = "application/json";
            request.Method = Method;

            _callback = callback;

            var responseTask = Task.Factory.FromAsync<WebResponse>
                                      (request.BeginGetResponse,
                                       request.EndGetResponse,
                                       null);

            using (var response = (HttpWebResponse)await responseTask)
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string content = streamReader.ReadToEnd();
                    _callback(Newtonsoft.Json.JsonConvert.DeserializeObject<RootMenuApi>(content));
                }
            }
            
        }
    }
}
