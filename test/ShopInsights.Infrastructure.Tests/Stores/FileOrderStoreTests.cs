using System.Linq;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ShopInsights.Core;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;
using ShopInsights.Infrastructure.Stores;
using Xunit;

namespace ShopInsights.Infrastructure.Tests.Stores
{
    public class FileOrderStoreTests : WithSubject<OrderFilesImporter>
    {
        public FileOrderStoreTests()
        {
            Services.AddCoreServices();
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

            await Subject.ImportExistingOrdersAsync();

            The<IOrderStorage>().AllOrders.Count().Should().Be(666);
        }
    }
}
