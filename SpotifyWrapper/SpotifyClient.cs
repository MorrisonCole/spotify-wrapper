using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using RestSharp;
using SpotifyWrapper.Configuration;

namespace SpotifyWrapper
{
    public class SpotifyClient
    {
        private readonly IRestClient restClient;
        private readonly ISpotifyConfiguration spotifyConfiguration;

        public SpotifyClient(IRestClient restClient, ISpotifyConfiguration spotifyConfiguration)
        {
            this.restClient = restClient;
            this.spotifyConfiguration = spotifyConfiguration;
        }

        public string GetAuthenticationUrl()
        {
            var request = new RestRequest(spotifyConfiguration.AuthorizeUrl, Method.GET);
            request.AddQueryParameter("client_id", spotifyConfiguration.ClientId);
            request.AddQueryParameter("response_type", "token");
            request.AddQueryParameter("redirect_uri", spotifyConfiguration.RedirectUri);
            
            return restClient.BuildUri(request).AbsoluteUri;
        }
    }
}