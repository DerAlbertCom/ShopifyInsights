using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.Loaders
{
    public class OrderLoader : Loader<Order>, IOrderLoader
    {
        public OrderLoader(IOptions<StoreOptions> optionsAccessor, IOrderImporter importer,
            IOrderStorage storage, IOrderFilesStorage filesStorage, ILogger<OrderLoader> logger) : base(optionsAccessor, importer, storage,
            filesStorage, "orders", order => order.UpdatedAt, logger)
        {
        }
    }
}