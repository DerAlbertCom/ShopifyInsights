using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class MetaFieldFetchAndStoreService : FetchAndStoreService<MetaField>, IMetaFieldFetchAndStoreService
    {
        public MetaFieldFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IMetaFieldShopifyFetcher shopifyFetcher,
            IMetaFieldStorage storage, IMetaFieldFilesWriter filesWriter, ILogger<MetaFieldFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesWriter, "metafields", MetaField => MetaField.UpdatedAt, logger)
        {
        }
    }
    public class LocationFetchAndStoreService : FetchAndStoreService<Location>, ILocationFetchAndStoreService
    {
        public LocationFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, ILocationShopifyFetcher shopifyFetcher,
            ILocationStorage storage, ILocationFilesWriter filesWriter, ILogger<LocationFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesWriter, "locations", Location => Location.UpdatedAt, logger)
        {
        }
    }
}
