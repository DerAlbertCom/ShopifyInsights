using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class OrderStorage : ShopifyStorage<Order>, IOrderStorage
    {
        public OrderStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, o=>o.CreatedAt, o=>o.UpdatedAt)
        {
        }
    }
}