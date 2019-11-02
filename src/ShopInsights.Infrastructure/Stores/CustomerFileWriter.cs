using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class CustomerFileWriter : FilesWriter<Customer>, ICustomerFilesWriter
    {
        public CustomerFileWriter(ICustomerStorage storage, ILogger<CustomerFileWriter> logger) : base(storage, "customers", logger)
        {
        }
    }
}
