using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    public interface IShopifyProductService
    {
        Task<IReadOnlyCollection<Product>> ListUpdatedSinceAsync(DateTimeOffset sinceDate);
    }
}