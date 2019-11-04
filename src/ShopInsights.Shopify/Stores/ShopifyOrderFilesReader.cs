using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyOrderFilesReader : ShopifyFilesReader<Order>, IShopifyOrderFilesReader
    {
        public ShopifyOrderFilesReader(IShopifyOrderStorage storage, ILogger<ShopifyOrderFilesReader> logger):base(storage,"orders", logger)
        {

        }
    }
}
