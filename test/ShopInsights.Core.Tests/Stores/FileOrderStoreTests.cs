using System.Linq;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ShopInsights.Core.Stores;
using Xunit;

namespace ShopInsights.Core.Tests.Stores
{
    public class FileOrderStoreTests : WithSubject<FileOrderStore>
    {
        public FileOrderStoreTests()
        {
            Services.AddOptions();
        }

        [Fact]
        public async Task Should_load_to_Orders()
        {
            Services.AddOptions<OrderStoreOptions>()
                .Configure(o =>
                    {
                        o.ImportPath =
                            "C:\\dev\\private\\ShopifyMetaFieldEditor\\ShopifyMetafieldEditor";
                    });

            var orders = await Subject.ImportExistingOrdersAsync();

            orders.Count.Should().Be(666);
        }
    }
}
