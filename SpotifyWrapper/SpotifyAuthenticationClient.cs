using System;
using System.Text;
using RestSharp;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Model.Server;

namespace SpotifyWrapper
{
    public class SpotifyAuthenticationClient
    {
        private readonly IRestClient restClient;
        private readonly ISpotifyConfiguration spotifyConfiguration;

        public SpotifyAuthenticationClient(IRestClient restClient, ISpotifyConfiguration spotifyConfiguration)
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
            var request = new RestRequest(spotifyConfiguration.TokenUrl, Method.POST);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", spotifyConfiguration.RedirectUri);

            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{spotifyConfiguration.ClientId}:{spotifyConfiguration.ClientSecret}"))}");

            return restClient.Execute<SpotifyCode>(request).Data;
        }
    }
}