using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyCustomerFileWriter : ShopifyFilesWriter<Customer>, IShopifyCustomerFilesWriter
    {
        public ShopifyCustomerFileWriter(IShopifyCustomerStorage storage, ILogger<ShopifyCustomerFileWriter> logger) : base(storage, "customers", logger)
        {
        }
    }
}
