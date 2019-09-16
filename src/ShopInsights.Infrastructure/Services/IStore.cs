using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Infrastructure.Services
{
    public interface IStore
    {
        Task<bool> StoreOrders(IEnumerable<Order> orders);
    }
}