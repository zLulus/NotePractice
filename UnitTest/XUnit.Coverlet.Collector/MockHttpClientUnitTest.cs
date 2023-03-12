using NSubstitute;
using Shouldly;
using System.Net;
using UnitTest.ClassLibrary;

namespace XUnit.Coverlet.Collector
{
    public class MockHttpClientUnitTest
    {
        HttpClient httpClient;
        const string returnMessage = "Ok";
        public MockHttpClientUnitTest()
        {
            var mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
            httpClient = new HttpClient(mockHttpMessageHandler);
            //mock response
            HttpResponseMessage mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            mockResponse.Content = new StringContent(returnMessage);
            mockHttpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
               .ReturnsForAnyArgs(mockResponse);
        }

        [Fact]
        public async void Test()
        {
            (await (await httpClient.GetAsync("https://cn.bing.com/")).Content.ReadAsStringAsync()).ShouldBe(returnMessage);
        }
    }
}
