using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class CustomerShopifyFileWriter : ShopifyFilesWriter<Customer>, ICustomerShopifyFilesWriter
    {
        public CustomerShopifyFileWriter(IShopifyCustomerStorage storage, ILogger<CustomerShopifyFileWriter> logger) : base(storage, "customers", logger)
        {
        }
    }
}
