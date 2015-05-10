using SpotifyWrapper.Configuration;

namespace SpotifyWrapper.Tests.Stubs
{
    public class StubSpotifyConfiguration : ISpotifyConfiguration
    {
        private readonly string clientId;
        private readonly string redirectUri;
        private readonly string authorizeUrl;

        public StubSpotifyConfiguration(string authorizeUrl, string clientId, string redirectUri)
        {
            this.authorizeUrl = authorizeUrl;
            this.clientId = clientId;
            this.redirectUri = redirectUri;
        }

        public string GetAuthorizeUrl()
        {
            return authorizeUrl;
        }

        public string GetClientId()
        {
            return clientId;
        }

        public string GetRedirectUri()
        {
            return redirectUri;
        }
    }
}