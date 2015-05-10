using SpotifyWrapper.Configuration;

namespace SpotifyWrapper.Tests.Stubs
{
    public class StubSpotifyConfiguration : ISpotifyConfiguration
    {
        private readonly string authorizeUrl;

        public StubSpotifyConfiguration(string authorizeUrl)
        {
            this.authorizeUrl = authorizeUrl;
        }

        public string GetAuthorizeUrl()
        {
            return authorizeUrl;
        }
    }
}