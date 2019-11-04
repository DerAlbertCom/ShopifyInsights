using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyCustomerFilesReader : ShopifyFilesReader<Customer>, IShopifyCustomerFilesReader
    {
        public ShopifyCustomerFilesReader(IShopifyCustomerStorage storage, ILogger<ShopifyCustomerFilesReader> logger):base(storage,"customers", logger)
        {

        }
    }
}
