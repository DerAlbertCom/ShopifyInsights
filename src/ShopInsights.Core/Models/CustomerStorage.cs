using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class CustomerStorage : ShopifyStorage<Customer>, ICustomerStorage
    {
        public CustomerStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, c =>c .CreatedAt, c=> c.UpdatedAt)
        {
        }
    }
}
