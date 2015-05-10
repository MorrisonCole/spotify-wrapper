using System;
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
        private const string ClientId = "a_client_id";
        private const string RedirectUri = "a://redirect.uri";
        private readonly Mock<IRestClient> mockRestClient = new Mock<IRestClient>();
        private readonly ISpotifyConfiguration configuration = new StubSpotifyConfiguration(AuthorizeUrl, ClientId, RedirectUri);

        [Test]
        public void Authenticates()
        {
            var spotifyClient = new SpotifyClient(mockRestClient.Object, configuration);

            spotifyClient.Authenticate();

            var expectedRestRequest = new RestRequest
            {
                Resource = AuthorizeUrl,
                Method = Method.GET,
                Parameters =
                {
                    new Parameter
                    {
                        Name = "client_id",
                        Value = ClientId
                    },
                    new Parameter
                    {
                        Name = "response_type",
                        Value = "token"
                    },
                    new Parameter
                    {
                        Name = "redirect_uri",
                        Value = RedirectUri
                    }
                }
            };

            mockRestClient.Verify(restClient => restClient.Execute(It.Is<IRestRequest>(actualRestRequest => MatchingRestRequest(actualRestRequest, expectedRestRequest))));
        }

        private static bool MatchingRestRequest(IRestRequest actual, IRestRequest expected)
        {
            foreach (var expectedParameter in expected.Parameters)
            {
                Assert.That(actual.Parameters.Exists(actualParameter => MatchingParameter(actualParameter, expectedParameter)));
            }

            Assert.AreEqual(actual.Resource, expected.Resource);
            Assert.AreEqual(actual.Method, expected.Method);

            return true;
        }

        private static bool MatchingParameter(Parameter actualParameter, Parameter expectedParameter)
        {
            return expectedParameter.Name.Equals(actualParameter.Name)
                && expectedParameter.Value.Equals(actualParameter.Value);
        }
    }
}