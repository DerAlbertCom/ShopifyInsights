using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class OrderFetchAndStoreService : FetchAndStoreService<Order>, IOrderFetchAndStoreService
    {
        public OrderFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IOrderShopifyFetcher shopifyFetcher,
            IOrderStorage storage, IOrderFilesWriter filesWriter, ILogger<OrderFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesWriter, "orders", order => order.UpdatedAt, logger)
        {
        }
    }
}