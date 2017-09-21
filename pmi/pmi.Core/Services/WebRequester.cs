using CoreFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pmi.Core.Services
{
    public static class WebRequester
    {
        public delegate void RequestDone(string response);

        private static RequestDone _callback;

        private static Dictionary<string, dynamic> Parameters { get; set; }

        private static string boundary = "----------" + DateTime.Now.Ticks.ToString();

        public static void GetResponse(string url, string contentType, string method, RequestDone callback, Dictionary<string, dynamic> parameters = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            
            Debug.WriteLine("start request " + contentType + " - " + method + " - " + url);
            request.ContentType = String.Format("{0}; boundary={1}", contentType, boundary);
            request.Method = method;
            
            Parameters = parameters;

            _callback = callback;

            if (Parameters != null && (request.Method == "POST" || request.Method == "PUT"))
            {
                request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
            }
            else
            {
                request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
            }
                
        }

        private async static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            asynchronousResult.AsyncWaitHandle.WaitOne(30000);
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            
            Debug.WriteLine("GetRequestStreamCallback");
            
            using (var postStream = webRequest.EndGetRequestStream(asynchronousResult))
            {
                Debug.WriteLine("EndGetRequestStream ");

                WriteMultipartObject(postStream);

            }
            
            Debug.WriteLine("send request... ");

            try
            {
                //var res = webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);
                var res = webRequest.BeginGetResponse(null, null);
                Debug.WriteLine("AsyncWaitHandle... ");
                res.AsyncWaitHandle.WaitOne(30000);

                HttpWebResponse response = (HttpWebResponse)webRequest.EndGetResponse(res);
                Stream streamResponse = response.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);

                string result = await streamRead.ReadToEndAsync();

                await streamResponse.FlushAsync();

                _callback(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("Exception... ");
                _callback(string.Empty);
            }
            finally
            {
                Parameters = new Dictionary<string, dynamic>();
            }
            

        }

        static void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            Debug.WriteLine("GetResponseCallback");
            try
            {
                HttpWebRequest myrequest = (HttpWebRequest)asynchronousResult.AsyncState;

                using (HttpWebResponse response = (HttpWebResponse)myrequest.EndGetResponse(asynchronousResult))
                {
                    Stream responseStream = response.GetResponseStream();

                    using (var reader = new StreamReader(responseStream))
                    {
                        string data = reader.ReadToEnd();

                        _callback(data);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("errooooor");
                AppManager.CurrentApplication.Error = new Exception(Translator.GetText("error_no_internet"));

                _callback(string.Empty);
            }

        }

        static void WriteMultipartObject(Stream stream)
        {
            Debug.WriteLine("WriteMultipartObject");
            using (StreamWriter writer = new StreamWriter(stream))
            {
                if (Parameters != null)
                {
                    foreach (var entry in Parameters as Dictionary<string, object>)
                    {
                        WriteEntry(writer, entry.Key, entry.Value);
                    }
                }

                writer.Write("--");
                writer.Write(boundary);
                writer.WriteLine("--");
                writer.Flush();
            }
        }

        private static void WriteEntry(StreamWriter writer, string key, object value)
        {
            if (value != null)
            {
                writer.Write("--");
                writer.WriteLine(boundary);

                if (value is byte[])
                {
                    byte[] ba = value as byte[];

                    writer.WriteLine(@"Content-Disposition: form-data; name=""{0}""; filename=""{1}""", key, "sentPhoto.jpg");
                    writer.WriteLine(@"Content-Type: application/octet-stream");
                    writer.WriteLine(@"Content-Type: image / jpeg");
                    writer.WriteLine(@"Content-Length: " + ba.Length);

                    writer.WriteLine();

                    writer.Flush();

                    Stream output = writer.BaseStream;

                    output.Write(ba, 0, ba.Length);
                    output.Flush();
                    writer.WriteLine();
                }
                else
                {
                    writer.WriteLine(@"Content-Disposition: form-data; name=""{0}""", key);
                    writer.WriteLine();
                    writer.WriteLine(value.ToString());
                }
            }
        }
    }
}
