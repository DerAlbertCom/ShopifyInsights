using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

// ReSharper disable PossibleMultipleEnumeration

namespace ShopInsights.Infrastructure.Services
{
    public interface ICachedOrderService
    {
        Task<IEnumerable<Order>> ListAsync();
    }
}
