using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify.Services.FetchAndStore
{
    public class ShopifyCustomerShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Customer>, IShopifyCustomerShopifyFetchAndStoreService
    {
        public ShopifyCustomerShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IShopifyCustomerFetcher fetcher,
            IShopifyCustomerStorage storage, IShopifyCustomerFilesWriter filesWriter, ILogger<ShopifyCustomerShopifyFetchAndStoreService> logger) : base(optionsAccessor, fetcher, storage,
            filesWriter, "customers", customer => customer.UpdatedAt, logger)
        {
        }
    }
}
