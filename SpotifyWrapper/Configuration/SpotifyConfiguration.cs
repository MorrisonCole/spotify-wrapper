namespace SpotifyWrapper.Configuration
{
    public class SpotifyConfiguration : ISpotifyConfiguration
    {
        public SpotifyConfiguration(string redirectUri)
        {
            RedirectUri = redirectUri;
        }

        public string AuthorizeUrl => "authorize";

        public string ClientId => "253269f512e245f196f62502f5e13911";

        public string ClientSecret => "3465364c1fd745a0871c38a1f8fb8858";

        public string RedirectUri { get; }
    }
}