using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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

        [Fact(Skip = "the files are not present in the repository, should be changed. or removed.")]
        public async Task Should_load_to_Orders()
        {
            await Subject.ImportExistingAsync("C:\\dev\\private\\ShopifyMetaFieldEditor\\ShopifyMetafieldEditor", CancellationToken.None);

            The<IShopifyOrderStorage>().All.Count().Should().Be(870);
        }
    }
}
