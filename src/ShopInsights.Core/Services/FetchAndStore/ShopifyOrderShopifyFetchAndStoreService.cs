using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class ShopifyOrderShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Order>, IShopifyOrderShopifyFetchAndStoreService
    {
        public ShopifyOrderShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IOrderShopifyFetcher shopifyFetcher,
            IShopifyOrderStorage storage, IShopifyOrderFilesWriter shopifyFilesWriter, ILogger<ShopifyOrderShopifyFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            shopifyFilesWriter, "orders", order => order.UpdatedAt, logger)
        {
        }
    }
}
