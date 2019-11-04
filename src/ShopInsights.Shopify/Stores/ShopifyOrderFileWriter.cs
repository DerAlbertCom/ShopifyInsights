using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyOrderFileWriter : ShopifyFilesWriter<Order>, IShopifyOrderFilesWriter
    {
        public ShopifyOrderFileWriter(IShopifyOrderStorage storage, ILogger<ShopifyOrderFileWriter> logger) : base(storage, "orders", logger)
        {
        }
    }
}
