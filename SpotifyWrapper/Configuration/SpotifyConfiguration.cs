namespace SpotifyWrapper.Configuration
{
    public class SpotifyConfiguration : ISpotifyConfiguration
    {
        public string GetAuthorizeUrl()
        {
            return "https://accounts.spotify.com/authorize";
        }

        public string GetClientId()
        {
            return "253269f512e245f196f62502f5e13911";
        }

        public string GetRedirectUri()
        {
            return "localhost:8080";
        }
    }
}