using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Services.Shopify;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify.Services.FetchAndStore
{
    public class LocationShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Location>, ILocationShopifyFetchAndStoreService
    {
        public LocationShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IShopifyLocationFetcher fetcher,
            ILocationStorage storage, IShopifyLocationFilesWriter filesWriter, ILogger<LocationShopifyFetchAndStoreService> logger) : base(optionsAccessor, fetcher, storage,
            filesWriter, "locations", Location => Location.UpdatedAt, logger)
        {
        }
    }
}