using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify.Services.FetchAndStore
{
    public class ShopifyOrderShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Order>, IShopifyOrderShopifyFetchAndStoreService
    {
        public ShopifyOrderShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IShopifyOrderFetcher fetcher,
            IShopifyOrderStorage storage, IShopifyOrderFilesWriter shopifyFilesWriter, ILogger<ShopifyOrderShopifyFetchAndStoreService> logger) : base(optionsAccessor, fetcher, storage,
            shopifyFilesWriter, "orders", order => order.UpdatedAt, logger)
        {
        }
    }
}
