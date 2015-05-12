using RestSharp.Deserializers;

namespace SpotifyWrapper.Model.Server
{
    public class SpotifyTrack
    {
        [DeserializeAs(Name = "preview_url")]
        public string PreviewUrl { get; set; }
    }
}