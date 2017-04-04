
using System;

namespace pmi.Core.Services
{
    public static class JsonRequester<T> where T : class
    {
        public static string Method => "GET";

        public static T Response;

        public delegate void RequestDone(T result);

        private static RequestDone _callback;
        
        public static void Request(string url, RequestDone callback = null)
        {
            _callback = callback;

            WebRequester.GetResponse(url, "application/json", Method, GetResponse);
        }

        private static void GetResponse(string response)
        {
            try
            {
                Response = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response);
            }
            catch
            {
                Response = Activator.CreateInstance(typeof(T)) as T;
            }
            
            _callback?.Invoke(Response);
        }
    }
}
