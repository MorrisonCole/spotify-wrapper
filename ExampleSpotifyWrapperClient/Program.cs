using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private const string LocalHost = "http://localhost:8888/";

        delegate void AuthenticateSpotifyCaller();
        private static Process authProcess;
        private static string code;

        static void Main(string[] args)
        {
            var restClient = new RestClient("https://accounts.spotify.com");
            var spotifyConfiguration = new SpotifyConfiguration(LocalHost + "spotify-callback");
            var spotifyWrapper = new SpotifyClient(restClient, spotifyConfiguration);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();

            authProcess = Process.Start(spotifyWrapper.GetAuthenticationUrl());

            var counter = 0;
            Console.Write("Awaiting auth code ");
            while (code == null)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); counter = 0; break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Thread.Sleep(100);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }

            Console.WriteLine($"\nGot auth code: {code}");
            Console.In.Read();
        }

        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            code = (string) e.Result;
        }

        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var simpleHttpServer = new SimpleHttpServer(LocalHost);
            e.Result = simpleHttpServer.Start();
        }
    }
}
