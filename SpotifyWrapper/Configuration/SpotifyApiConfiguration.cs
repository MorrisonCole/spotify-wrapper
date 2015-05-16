namespace SpotifyWrapper.Configuration
{
    internal class SpotifyApiConfiguration : ISpotifyApiConfiguration
    {
        public SpotifyApiConfiguration(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string TrackUrl => "tracks";

        public string AccessToken { get; }
    }
}