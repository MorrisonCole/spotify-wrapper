namespace SpotifyWrapper.Configuration
{
    public class SpotifyConfiguration : ISpotifyConfiguration
    {
        public string GetAuthorizeUrl()
        {
            return "https://accounts.spotify.com/authorize";
        }
    }
}