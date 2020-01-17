using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Services;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyOrderStorage : ShopifyStorage<Order>, IShopifyOrderStorage
    {
        public ShopifyOrderStorage(IOptions<ShopInstanceOptions> optionsAccessor,
            ISourceDataChangedService changedService) : base(changedService, optionsAccessor, o => o.CreatedAt,
            o => o.UpdatedAt)
        {
        }
    }
}
