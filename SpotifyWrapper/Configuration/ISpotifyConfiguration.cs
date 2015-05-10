namespace SpotifyWrapper.Configuration
{
    public interface ISpotifyConfiguration
    {
        string GetAuthorizeUrl();
        string GetClientId();
        string GetRedirectUri();
    }
}