using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class ShopifyCustomerShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Customer>, IShopifyCustomerShopifyFetchAndStoreService
    {
        public ShopifyCustomerShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, ICustomerShopifyFetcher shopifyFetcher,
            IShopifyCustomerStorage storage, ICustomerShopifyFilesWriter shopifyFilesWriter, ILogger<ShopifyCustomerShopifyFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            shopifyFilesWriter, "customers", customer => customer.UpdatedAt, logger)
        {
        }
    }
}
