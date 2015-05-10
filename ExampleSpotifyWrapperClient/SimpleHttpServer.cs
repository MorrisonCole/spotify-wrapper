using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExampleSpotifyWrapperClient
{
    internal class SimpleHttpServer
    {
        private readonly HttpListener httpListener;

        public SimpleHttpServer(string url)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(url);
        }

        public void Start()
        {
            httpListener.Start();

            while (httpListener.IsListening)
                ProcessRequest();
        }

        public void Stop()
        {
            httpListener.Stop();
        }

        private void ProcessRequest()
        {
            var result = httpListener.BeginGetContext(ListenerCallback, httpListener);
            result.AsyncWaitHandle.WaitOne();
        }

        private void ListenerCallback(IAsyncResult result)
        {
            var context = httpListener.EndGetContext(result);
            var info = Read(context.Request);

            Console.WriteLine("Server received: \n{0}", info);

            CreateResponse(context.Response, info.ToString());
        }

        public static WebRequestInfo Read(HttpListenerRequest request)
        {
            var info = new WebRequestInfo
            {
                HttpMethod = request.HttpMethod,
                Url = request.Url
            };

            if (request.HasEntityBody)
            {
                var encoding = request.ContentEncoding;
                using (var bodyStream = request.InputStream)
                using (var streamReader = new StreamReader(bodyStream, encoding))
                {
                    if (request.ContentType != null)
                        info.ContentType = request.ContentType;

                    info.ContentLength = request.ContentLength64;
                    info.Body = streamReader.ReadToEnd();
                }
            }

            return info;
        }

        public static WebResponseInfo Read(HttpWebResponse response)
        {
            var info = new WebResponseInfo
            {
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription,
                ContentEncoding = response.ContentEncoding,
                ContentLength = response.ContentLength,
                ContentType = response.ContentType
            };

            using (var bodyStream = response.GetResponseStream())
            using (var streamReader = new StreamReader(bodyStream, Encoding.UTF8))
            {
                info.Body = streamReader.ReadToEnd();
            }

            return info;
        }

        private static void CreateResponse(HttpListenerResponse response, string body)
        {
            response.StatusCode = (int) HttpStatusCode.OK;
            response.StatusDescription = HttpStatusCode.OK.ToString();
            var buffer = Encoding.UTF8.GetBytes(body);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }

    public class WebRequestInfo
    {
        public string Body { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public string HttpMethod { get; set; }
        public Uri Url { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"HttpMethod {HttpMethod}");
            sb.AppendLine($"Url {Url}");
            sb.AppendLine($"ContentType {ContentType}");
            sb.AppendLine($"ContentLength {ContentLength}");
            sb.AppendLine($"Body {Body}");
            return sb.ToString();
        }
    }

    public class WebResponseInfo
    {
        public string Body { get; set; }
        public string ContentEncoding { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"StatusCode {StatusCode} StatusDescripton {StatusDescription}");
            sb.AppendLine($"ContentType {ContentType} ContentEncoding {ContentEncoding} ContentLength {ContentLength}");
            sb.AppendLine($"Body {Body}");
            return sb.ToString();
        }
    }
}