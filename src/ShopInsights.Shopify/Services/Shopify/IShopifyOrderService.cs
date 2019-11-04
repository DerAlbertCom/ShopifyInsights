using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services.Shopify
{
    public interface IShopifyOrderService
    {
        Task<IReadOnlyCollection<Order>> ListUpdatedSinceAsync(DateTimeOffset sinceDate);
    }
}