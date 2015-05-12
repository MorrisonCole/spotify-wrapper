using SpotifyWrapper.Model.Server;

namespace SpotifyWrapper.Configuration
{
    internal class SpotifyApiConfiguration : ISpotifyApiConfiguration
    {
        private readonly SpotifyCode spotifyCode;

        public SpotifyApiConfiguration(SpotifyCode spotifyCode)
        {
            this.spotifyCode = spotifyCode;
        }

        public string TrackUrl => "tracks";

        public string AccessToken => spotifyCode.AccessToken;
    }
}