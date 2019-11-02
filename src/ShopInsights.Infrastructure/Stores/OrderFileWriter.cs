using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class OrderFileWriter : FilesWriter<Order>, IOrderFilesWriter
    {
        public OrderFileWriter(IOrderStorage storage, ILogger<OrderFileWriter> logger) : base(storage, "orders", logger)
        {
        }
    }
}
