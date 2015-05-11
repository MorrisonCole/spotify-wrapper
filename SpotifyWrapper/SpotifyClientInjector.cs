using RestSharp;
using SpotifyWrapper.Configuration;

namespace SpotifyWrapper
{
    public class SpotifyClientInjector
    {
        public static SpotifyClient SpotifyClient(ISpotifyConfiguration spotifyConfiguration)
        {
            return new SpotifyClient(new RestClient("https://accounts.spotify.com"), spotifyConfiguration);
        }
    }
}