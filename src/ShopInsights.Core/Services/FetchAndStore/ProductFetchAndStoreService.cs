using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public class ProductFetchAndStoreService : FetchAndStoreService<Product>, IProductFetchAndStoreService
    {
        public ProductFetchAndStoreService(IOptions<StoreOptions> optionsAccessor, IProductShopifyFetcher shopifyFetcher,
            IProductStorage storage, IProductFilesWriter filesWriter, ILogger<ProductFetchAndStoreService> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesWriter, "products", product => product.UpdatedAt, logger)
        {
        }
    }
}
