using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyMetaFieldStorage : ShopifyStorage<MetaField>, IShopifyMetaFieldStorage
    {
        public ShopifyMetaFieldStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, c =>c .CreatedAt, c=> c.UpdatedAt)
        {
        }
    }
}
