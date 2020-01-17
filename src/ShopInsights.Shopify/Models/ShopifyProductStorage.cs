using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Services;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyProductStorage : ShopifyStorage<Product>, IShopifyProductStorage
    {
        public ShopifyProductStorage(IOptions<ShopInstanceOptions> optionsAccessor,
            ISourceDataChangedService changedService) : base(changedService, optionsAccessor, p => p.CreatedAt,
            p => p.UpdatedAt)
        {
        }
    }
}
