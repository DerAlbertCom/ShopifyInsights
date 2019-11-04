using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyOrderFilesReader : ShopifyFilesReader<Order>, IShopifyOrderFilesReader
    {
        public ShopifyOrderFilesReader(IShopifyOrderStorage storage, ILogger<ShopifyOrderFilesReader> logger):base(storage,"orders", logger)
        {

        }
    }
}
