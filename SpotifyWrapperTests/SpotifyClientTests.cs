using Moq;
using NUnit.Framework;
using RestSharp;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Tests.Stubs;

namespace SpotifyWrapper.Tests
{
    [TestFixture]
    public class SpotifyClientTests
    {
        private const string AuthorizeUrl = "http://authorize.url";
        private readonly Mock<IRestClient> mockRestClient = new Mock<IRestClient>();
        private readonly ISpotifyConfiguration configuration = new StubSpotifyConfiguration(AuthorizeUrl);

        [Test]
        public void Authenticates()
        {
            var spotifyClient = new SpotifyClient(mockRestClient.Object, configuration);

            spotifyClient.Authenticate();

            var expectedRestRequest = new RestRequest
            {
                Resource = AuthorizeUrl,
                Method = Method.GET
            };

            mockRestClient.Verify(restClient => restClient.Execute(It.Is<IRestRequest>(actualRestRequest => MatchingRestRequest(actualRestRequest, expectedRestRequest))));
        }

        private bool MatchingRestRequest(IRestRequest actual, IRestRequest expected)
        {
            return actual.Resource == expected.Resource
                && actual.Method == expected.Method;
        }
    }
}