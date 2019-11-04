using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyOrderStorage : ShopifyStorage<Order>, IShopifyOrderStorage
    {
        public ShopifyOrderStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, o=>o.CreatedAt, o=>o.UpdatedAt)
        {
        }
    }
}
