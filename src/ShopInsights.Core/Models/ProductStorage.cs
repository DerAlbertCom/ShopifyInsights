using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class ProductStorage : ShopifyStorage<Product>, IProductStorage
    {
        public ProductStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, p=>p.CreatedAt, p=>p.UpdatedAt)
        {
        }
    }
}