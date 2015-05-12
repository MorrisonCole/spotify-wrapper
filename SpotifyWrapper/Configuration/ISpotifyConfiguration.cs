namespace SpotifyWrapper.Configuration
{
    public interface ISpotifyConfiguration
    {
        string AuthorizeUrl { get; }
        string ClientId { get; }
        string RedirectUri { get; }
        string ClientSecret { get; }
        string TokenUrl { get; }
    }
}