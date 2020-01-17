using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Services;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyShopifyLocationStorage : ShopifyStorage<Location>, IShopifyLocationStorage
    {
        public ShopifyShopifyLocationStorage(IOptions<ShopInstanceOptions> optionsAccessor,
            ISourceDataChangedService changedService) : base(changedService, optionsAccessor, c => c.CreatedAt,
            c => c.UpdatedAt)
        {
        }
    }
}
