using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ShopInsights.Web.Tests.Controllers
{
    public class PingControllerTests : ControllerTests
    {
        readonly HttpClient _client;

        public PingControllerTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_ping_the_controller()
        {
            var response = await _client.GetAsync("/api/ping");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());

            result.Should().Be(100);
        }
    }
}
