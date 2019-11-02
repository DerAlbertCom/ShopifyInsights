using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class CustomerFilesImporter : FilesImporter<Customer>, ICustomerFilesImporter
    {
        public CustomerFilesImporter(ICustomerStorage storage, ILogger<CustomerFilesImporter> logger):base(storage,"customers", logger)
        {

        }
    }
}