using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class ShopifyProductStorage : ShopifyStorage<Product>, IShopifyProductStorage
    {
        public ShopifyProductStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, p=>p.CreatedAt, p=>p.UpdatedAt)
        {
        }
    }
}
