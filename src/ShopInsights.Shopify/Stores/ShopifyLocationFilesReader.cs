using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyLocationFilesReader : ShopifyFilesReader<Location>, IShopifyLocationFilesReader
    {
        public ShopifyLocationFilesReader(IShopifyLocationStorage storage, ILogger<ShopifyLocationFilesReader> logger):base(storage,"locations", logger)
        {

        }
    }
}