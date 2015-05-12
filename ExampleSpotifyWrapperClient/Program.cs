using System;
using System.ComponentModel;
using System.Diagnostics;
using SpotifyWrapper;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Model.Server;

namespace ExampleSpotifyWrapperClient
{
    internal class Program
    {
        private const string LocalHostUrl = "http://localhost:8888/";
        private static string code;

        private static void Main()
        {
            var spotifyConfiguration = new SpotifyConfiguration(LocalHostUrl + "spotify-callback");
            var spotifyAuthenticationClient = SpotifyClientInjector.SpotifyAuthenticationClient(spotifyConfiguration);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += RetrieveSpotifyCode;
            backgroundWorker.RunWorkerCompleted += OnSpotifyCodeRetrieved;
            backgroundWorker.RunWorkerAsync();

            Process.Start(spotifyAuthenticationClient.GetAuthenticationUrl());

            var counter = 0;
            Console.Write("Awaiting auth code ");
            while (code == null)
            {
                counter++;
                switch (counter%4)
                {
                    case 0:
                        Console.Write("/");
                        counter = 0;
                        break;
                    case 1:
                        Console.Write("-");
                        break;
                    case 2:
                        Console.Write("\\");
                        break;
                    case 3:
                        Console.Write("|");
                        break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }

            Console.WriteLine($"\nGot auth code: {code}");

            var spotifyCode = spotifyAuthenticationClient.GetToken(code);

            var spotifyApiClient = SpotifyClientInjector.SpotifyApiClient(spotifyCode);
            var spotifyTrack = spotifyApiClient.GetTrack("2dLQO5zSiudyseIzh5VXI7");

            Console.WriteLine(spotifyTrack.PreviewUrl);
            Console.In.Read();
        }

        private static void RetrieveSpotifyCode(object sender, DoWorkEventArgs e)
        {
            var simpleHttpServer = new SpotifyCodeReceiverHttpListener(LocalHostUrl);
            e.Result = simpleHttpServer.Start();
        }

        private static void OnSpotifyCodeRetrieved(object sender, RunWorkerCompletedEventArgs e)
        {
            code = (string) e.Result;
        }
    }
}