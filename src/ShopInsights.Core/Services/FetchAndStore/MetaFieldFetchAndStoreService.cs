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
            filesWriter, "MetaFields", MetaField => MetaField.UpdatedAt, logger)
        {
        }
    }
}