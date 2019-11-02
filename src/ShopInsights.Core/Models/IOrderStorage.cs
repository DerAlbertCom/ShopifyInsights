using System;
using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Models
{
    public interface IOrderStorage : IShopifyStorage<Order>
    {
    }
}
