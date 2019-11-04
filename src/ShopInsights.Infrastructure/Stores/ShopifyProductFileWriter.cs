using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyProductFileWriter : ShopifyFilesWriter<Product>, IShopifyProductFilesWriter
    {
        public ShopifyProductFileWriter(IShopifyProductStorage storage, ILogger<ShopifyProductFileWriter> logger) : base(storage, "products", logger)
        {
        }
    }
}
