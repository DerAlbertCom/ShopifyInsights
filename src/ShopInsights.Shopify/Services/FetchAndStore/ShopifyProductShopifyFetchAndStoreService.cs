using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify.Services.FetchAndStore
{
    public class ShopifyProductShopifyFetchAndStoreService : ShopifyFetchAndStoreService<Product>, IShopifyProductShopifyFetchAndStoreService
    {
        public ShopifyProductShopifyFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IProductShopifyFetcher shopifyFetcher,
            IShopifyProductStorage storage, IShopifyProductFilesWriter filesWriter, ILogger<ShopifyProductShopifyFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesWriter, "products", product => product.UpdatedAt, logger)
        {
        }
    }
}
