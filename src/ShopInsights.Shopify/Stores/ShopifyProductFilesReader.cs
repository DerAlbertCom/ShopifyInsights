using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyProductFilesReader : ShopifyFilesReader<Product>, IShopifyProductFilesReader
    {
        public ShopifyProductFilesReader(IShopifyProductStorage storage, ILogger<ShopifyProductFilesReader> logger):base(storage,"products", logger)
        {

        }
    }
}
