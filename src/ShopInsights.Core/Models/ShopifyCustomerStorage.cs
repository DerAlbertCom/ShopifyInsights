using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class ShopifyCustomerStorage : ShopifyStorage<Customer>, IShopifyCustomerStorage
    {
        public ShopifyCustomerStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, c =>c .CreatedAt, c=> c.UpdatedAt)
        {
        }
    }
}
