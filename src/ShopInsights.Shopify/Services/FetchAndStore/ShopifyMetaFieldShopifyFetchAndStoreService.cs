using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class ShopifyMetaFieldShopifyFetchAndStoreService : ShopifyFetchAndStoreService<MetaField>, IShopifyMetaFieldShopifyFetchAndStoreService
    {
        public ShopifyMetaFieldShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IMetaFieldShopifyFetcher shopifyFetcher,
            IShopifyMetaFieldStorage storage, IShopifyMetaFieldFilesWriter shopifyFilesWriter, ILogger<ShopifyMetaFieldShopifyFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            shopifyFilesWriter, "metafields", MetaField => MetaField.UpdatedAt, logger)
        {
        }
    }
    public class LocationShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Location>, ILocationShopifyFetchAndStoreService
    {
        public LocationShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, ILocationShopifyFetcher shopifyFetcher,
            ILocationStorage storage, ILocationShopifyFilesWriter shopifyFilesWriter, ILogger<LocationShopifyFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            shopifyFilesWriter, "locations", Location => Location.UpdatedAt, logger)
        {
        }
    }
}
