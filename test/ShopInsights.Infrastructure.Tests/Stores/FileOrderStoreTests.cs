using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DerAlbert.Extensions.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ShopInsights.Core;
using ShopInsights.Core.Models;
using ShopInsights.Infrastructure.Stores;
using Xunit;

namespace ShopInsights.Infrastructure.Tests.Stores
{
    public class FileOrderStoreTests : WithSubject<OrderFilesReader>
    {
        public FileOrderStoreTests()
        {
            Services.AddCoreServices();
            Services.AddOptions();
        }

        [Fact(Skip = "the files are not present in the repository, should be changed. or removed.")]
        public async Task Should_load_to_Orders()
        {
            await Subject.ImportExistingAsync("C:\\dev\\private\\ShopifyMetaFieldEditor\\ShopifyMetafieldEditor", CancellationToken.None);

            The<IOrderStorage>().All.Count().Should().Be(870);
        }
    }
}
