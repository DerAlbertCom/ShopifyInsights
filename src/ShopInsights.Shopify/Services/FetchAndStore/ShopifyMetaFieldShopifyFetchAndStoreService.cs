using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify.Services.FetchAndStore
{
    public class ShopifyMetaFieldShopifyFetchAndStoreService : ShopifyFetchAndStoreService<MetaField>, IShopifyMetaFieldShopifyFetchAndStoreService
    {
        public ShopifyMetaFieldShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IShopifyMetaFieldFetcher fetcher,
            IShopifyMetaFieldStorage storage, IShopifyMetaFieldFilesWriter shopifyFilesWriter, ILogger<ShopifyMetaFieldShopifyFetchAndStoreService> logger) : base(optionsAccessor, fetcher, storage,
            shopifyFilesWriter, "metafields", MetaField => MetaField.UpdatedAt, logger)
        {
        }
    }
}
