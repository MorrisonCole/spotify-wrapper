using System;
using System.Text;
using RestSharp;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Model.Server;

namespace SpotifyWrapper
{
    public class SpotifyApiClient
    {
        private readonly IRestClient restClient;
        private readonly ISpotifyApiConfiguration spotifyApiConfiguration;

        public SpotifyApiClient(IRestClient restClient, ISpotifyApiConfiguration spotifyApiConfiguration)
        {
            this.restClient = restClient;
            this.spotifyApiConfiguration = spotifyApiConfiguration;
        }

        public SpotifyTrack GetTrack(string trackId)
        {
            var request = new RestRequest($"{spotifyApiConfiguration.TrackUrl}/{trackId}", Method.GET);

            return restClient.Execute<SpotifyTrack>(request).Data;
        }
    }
}