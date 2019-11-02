using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class OrderFileStorage : FilesStorage<Order>, IOrderFilesStorage
    {
        public OrderFileStorage(IOrderStorage storage, ILogger<OrderFileStorage> logger) : base(storage, "orders", logger)
        {
        }
    }
}
