using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Services.Shopify
{
    public interface IShopifyProductService
    {
        Task<IReadOnlyCollection<Product>> ListUpdatedSinceAsync(DateTimeOffset sinceDate);
    }
}