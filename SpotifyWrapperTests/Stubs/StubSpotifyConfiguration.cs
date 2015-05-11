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

        public string AuthorizeUrl
        {
            get { return authorizeUrl; }
        }

        public string ClientId
        {
            get { return clientId; }
        }

        public string RedirectUri
        {
            get { return redirectUri; }
        }

        public string ClientSecret { get; set; }
    }
}