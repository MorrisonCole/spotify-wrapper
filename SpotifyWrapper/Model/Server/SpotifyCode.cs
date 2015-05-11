using RestSharp.Deserializers;

namespace SpotifyWrapper.Model.Server
{
    public class SpotifyCode
    {
        [DeserializeAs(Name = "access_token")]
        public string AccessToken { get; set; }

        [DeserializeAs(Name = "token_type")]
        public string TokenType { get; set; }

        [DeserializeAs(Name = "expires_in")]
        public string ExpiresIn { get; set; }

        [DeserializeAs(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}