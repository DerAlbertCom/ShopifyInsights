using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    public interface IShopifyLocationService
    {
        Task<IReadOnlyCollection<Location>> ListUpdatedSinceAsync();

    }
}
