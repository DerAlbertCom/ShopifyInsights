using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    public interface IShopifyMetaFieldService
    {
        Task<IReadOnlyCollection<MetaField>> ListUpdatedSinceAsync(DateTimeOffset sinceDate);

    }
}
