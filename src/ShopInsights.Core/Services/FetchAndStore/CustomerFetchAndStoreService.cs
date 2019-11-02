using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class CustomerFetchAndStoreService : FetchAndStoreService<Customer>, ICustomerFetchAndStoreService
    {
        public CustomerFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, ICustomerShopifyFetcher shopifyFetcher,
            ICustomerStorage storage, ICustomerFilesWriter filesWriter, ILogger<CustomerFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesWriter, "customers", customer => customer.UpdatedAt, logger)
        {
        }
    }
}
