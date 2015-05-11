using System;
using System.Text;
using RestSharp;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Model.Server;

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
            request.AddQueryParameter("response_type", "code");
            request.AddQueryParameter("redirect_uri", spotifyConfiguration.RedirectUri);

            return restClient.BuildUri(request).AbsoluteUri;
        }

        public SpotifyCode GetToken(string code)
        {
            var request = new RestRequest(spotifyConfiguration.AuthorizeUrl, Method.GET);
            request.AddQueryParameter("grant_type", "authorization_code");
            request.AddQueryParameter("code", code);
            request.AddQueryParameter("redirect_uri", spotifyConfiguration.RedirectUri);

            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{spotifyConfiguration.ClientId}:{spotifyConfiguration.ClientSecret}"))}");

            return restClient.Execute<SpotifyCode>(request).Data;
        }
    }
}