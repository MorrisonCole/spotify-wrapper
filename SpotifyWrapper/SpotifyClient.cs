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

        public void Authenticate()
        {
            var request = new RestRequest(spotifyConfiguration.GetAuthorizeUrl(), Method.GET);
            request.AddQueryParameter("client_id", spotifyConfiguration.GetClientId());
            request.AddQueryParameter("response_type", "token");
            request.AddQueryParameter("redirect_uri", spotifyConfiguration.GetRedirectUri());

            var response = restClient.Execute(request);
        }
    }
}