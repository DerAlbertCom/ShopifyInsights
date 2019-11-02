using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Stores
{
    public interface IOrderStore
    {
        Task Store(IReadOnlyDictionary<int,Order> orders);
    }
}
