using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services.Shopify
{
    public interface IShopifyLocationService
    {
        Task<IReadOnlyCollection<Location>> ListUpdatedSinceAsync();

    }
}