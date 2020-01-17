using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyLocationFileWriter : ShopifyFilesWriter<Location>, IShopifyLocationFilesWriter
    {
        public ShopifyLocationFileWriter(IShopifyLocationStorage storage, ILogger<ShopifyLocationFileWriter> logger) : base(storage, "locations", logger)
        {
        }
    }
}