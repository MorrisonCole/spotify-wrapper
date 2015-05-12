namespace SpotifyWrapper.Configuration
{
    public interface ISpotifyApiConfiguration
    {
        string TrackUrl { get; }

        string AccessToken { get; }
    }
}