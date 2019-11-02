using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.Shopify
{
    public interface IOrderImporter : IImporter<Order>

    {
        Task<IReadOnlyCollection<Order>> GetOrderSinceId(long id);
    }
}
