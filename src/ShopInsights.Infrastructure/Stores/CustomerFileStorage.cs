using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class CustomerFileStorage : FilesStorage<Customer>, ICustomerFilesStorage
    {
        public CustomerFileStorage(ICustomerStorage storage, ILogger<CustomerFileStorage> logger) : base(storage, "customers", logger)
        {
        }
    }
}
