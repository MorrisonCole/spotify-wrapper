using RestSharp;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Model.Server;

namespace SpotifyWrapper
{
    public class SpotifyClientInjector
    {
        public static SpotifyAuthenticationClient SpotifyAuthenticationClient(ISpotifyConfiguration spotifyConfiguration)
        {
            return new SpotifyAuthenticationClient(new RestClient("https://accounts.spotify.com"), spotifyConfiguration);
        }

        public static SpotifyApiClient SpotifyApiClient(SpotifyCode spotifyCode)
        {
            var restClient = new RestClient("https://api.spotify.com/v1")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(spotifyCode.AccessToken, spotifyCode.TokenType)
            };

            return new SpotifyApiClient(restClient, new SpotifyApiConfiguration(spotifyCode.AccessToken));
        }
    }
}