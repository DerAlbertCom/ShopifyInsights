using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyCustomerFilesReader : ShopifyFilesReader<Customer>, IShopifyCustomerFilesReader
    {
        public ShopifyCustomerFilesReader(IShopifyCustomerStorage storage, ILogger<ShopifyCustomerFilesReader> logger):base(storage,"customers", logger)
        {

        }
    }
}
