using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ShopInsights.Web.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {

    }

    public class ControllerTests : IClassFixture<TestWebApplicationFactory>
    {

    }


}
