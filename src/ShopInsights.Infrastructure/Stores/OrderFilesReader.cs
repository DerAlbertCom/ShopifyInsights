using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class OrderFilesReader : FilesReader<Order>, IOrderFilesReader
    {
        public OrderFilesReader(IOrderStorage storage, ILogger<OrderFilesReader> logger):base(storage,"orders", logger)
        {

        }
    }
}
