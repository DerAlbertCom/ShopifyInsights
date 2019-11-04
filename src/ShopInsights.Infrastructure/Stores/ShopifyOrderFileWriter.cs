using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyOrderFileWriter : ShopifyFilesWriter<Order>, IShopifyOrderFilesWriter
    {
        public ShopifyOrderFileWriter(IShopifyOrderStorage storage, ILogger<ShopifyOrderFileWriter> logger) : base(storage, "orders", logger)
        {
        }
    }
}
