using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Services;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyCustomerStorage : ShopifyStorage<Customer>, IShopifyCustomerStorage
    {
        public ShopifyCustomerStorage(IOptions<ShopInstanceOptions> optionsAccessor,
            ISourceDataChangedService changedService) : base(changedService, optionsAccessor, c => c.CreatedAt,
            c => c.UpdatedAt)
        {
        }
    }
}
