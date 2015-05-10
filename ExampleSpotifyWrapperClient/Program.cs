using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using RestSharp;
using SpotifyWrapper;
using SpotifyWrapper.Configuration;

namespace ExampleSpotifyWrapperClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const string callbackUrl = "http://localhost:8888/spotify-callback/";

            var restClient = new RestClient("https://accounts.spotify.com");
            var spotifyConfiguration = new SpotifyConfiguration(callbackUrl);
            var spotifyWrapper = new SpotifyClient(restClient, spotifyConfiguration);
            
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                var simpleHttpServer = new SimpleHttpServer(callbackUrl);
                simpleHttpServer.Start();
            }).Start();

            Process.Start(spotifyWrapper.GetAuthenticationUrl());
        }
    }
}
