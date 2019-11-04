using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyProductFileWriter : ShopifyFilesWriter<Product>, IShopifyProductFilesWriter
    {
        public ShopifyProductFileWriter(IShopifyProductStorage storage, ILogger<ShopifyProductFileWriter> logger) : base(storage, "products", logger)
        {
        }
    }
}
