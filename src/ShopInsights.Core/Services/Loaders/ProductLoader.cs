using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.Loaders
{
    public class ProductLoader : Loader<Product>, IProductLoader
    {
        public ProductLoader(IOptions<StoreOptions> optionsAccessor, IProductImporter importer,
            IProductStorage storage, IProductFilesStorage filesStorage, ILogger<ProductLoader> logger) : base(optionsAccessor, importer, storage,
            filesStorage, "products", product => product.UpdatedAt, logger)
        {
        }
    }

    public class OrderLoader : Loader<Order>, IOrderLoader
    {
        public OrderLoader(IOptions<StoreOptions> optionsAccessor, IOrderImporter importer,
            IOrderStorage storage, IOrderFilesStorage filesStorage, ILogger<OrderLoader> logger) : base(optionsAccessor, importer, storage,
            filesStorage, "orders", order => order.UpdatedAt, logger)
        {
        }
    }

    public class CustomerLoader : Loader<Customer>, ICustomerLoader
    {
        public CustomerLoader(IOptions<StoreOptions> optionsAccessor, ICustomerImporter importer,
            ICustomerStorage storage, ICustomerFilesStorage filesStorage, ILogger<CustomerLoader> logger) : base(optionsAccessor, importer, storage,
            filesStorage, "customers", customer => customer.UpdatedAt, logger)
        {
        }
    }

}
