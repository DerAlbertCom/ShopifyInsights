using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Services.Shopify
{
    public interface IShopifyMetaFieldService
    {
        Task<IReadOnlyCollection<MetaField>> ListUpdatedSinceAsync(DateTimeOffset sinceDate);

    }
    public interface IShopifyLocationService
    {
        Task<IReadOnlyCollection<Location>> ListUpdatedSinceAsync();

    }
}
