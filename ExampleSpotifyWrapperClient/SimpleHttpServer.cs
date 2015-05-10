using System;
using System.Net;

namespace ExampleSpotifyWrapperClient
{
    internal class SimpleHttpServer
    {
        private static string code;
        private readonly HttpListener httpListener;

        public SimpleHttpServer(string url)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(url);
        }

        public string Start()
        {
            httpListener.Start();

            while (code == null)
            {
                var context = httpListener.GetContext();
                var query = context.Request.QueryString;
                code = query.Get("code");
                context.Response.Close();
            }

            httpListener.Stop();
            return code;
        }

        public void Stop()
        {
            httpListener.Stop();
        }
    }
}