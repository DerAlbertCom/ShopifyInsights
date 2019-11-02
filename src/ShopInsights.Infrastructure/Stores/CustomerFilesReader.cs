using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class CustomerFilesReader : FilesReader<Customer>, ICustomerFilesReader
    {
        public CustomerFilesReader(ICustomerStorage storage, ILogger<CustomerFilesReader> logger):base(storage,"customers", logger)
        {

        }
    }
}
