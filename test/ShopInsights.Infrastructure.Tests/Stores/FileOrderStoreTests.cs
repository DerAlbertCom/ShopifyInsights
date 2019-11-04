using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ShopInsights.Core;
using ShopInsights.Shopify;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;
using Xunit;

namespace ShopInsights.Infrastructure.Tests.Stores
{
    public class FileOrderStoreTests : WithSubject<ShopifyOrderFilesReader>
    {
        public FileOrderStoreTests()
        {
            Services.AddShopifyServices();
            Services.AddOptions();
        }

        [Fact]
        public async Task Should_load_to_Orders()
        {

            await Subject.ImportExistingAsync("C:\\dev\\private\\ShopifyMetaFieldEditor\\ShopifyMetafieldEditor", CancellationToken.None);

            The<IShopifyOrderStorage>().All.Count().Should().Be(870);
        }
    }
}
