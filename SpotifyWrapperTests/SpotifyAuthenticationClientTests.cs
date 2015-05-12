using System;
using Moq;
using NUnit.Framework;
using RestSharp;
using SpotifyWrapper.Configuration;
using SpotifyWrapper.Tests.Stubs;

namespace SpotifyWrapper.Tests
{
    [TestFixture]
    public class SpotifyAuthenticationClientTests
    {
        private const string AuthorizeUrl = "http://authorize.url";
        private const string ClientId = "a_client_id";
        private const string RedirectUri = "a://redirect.uri";
        private const string AuthenticationUrl = "http://authentication.uri/";

        private readonly Mock<IRestClient> mockRestClient = new Mock<IRestClient>();
        private readonly ISpotifyConfiguration configuration = new StubSpotifyConfiguration(AuthorizeUrl, ClientId, RedirectUri);

        [Test]
        public void GetsAuthenticationUrl()
        {
            var spotifyClient = new SpotifyAuthenticationClient(mockRestClient.Object, configuration);
            
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
                        Value = "code"
                    },
                    new Parameter
                    {
                        Name = "redirect_uri",
                        Value = RedirectUri
                    }
                }
            };

            mockRestClient.Setup(
                client =>
                    client.BuildUri(
                        It.Is<IRestRequest>(
                            actualRestRequest => MatchingRestRequest(actualRestRequest, expectedRestRequest))))
                .Returns(new Uri(AuthenticationUrl));

            Assert.AreEqual(spotifyClient.GetAuthenticationUrl(), AuthenticationUrl);
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