using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.Loaders
{
    public class CustomerLoader : Loader<Customer>, ICustomerLoader
    {
        public CustomerLoader(IOptions<StoreOptions> optionsAccessor, ICustomerShopifyFetcher shopifyFetcher,
            ICustomerStorage storage, ICustomerFilesStorage filesStorage, ILogger<CustomerLoader> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesStorage, "customers", customer => customer.UpdatedAt, logger)
        {
        }
    }
}