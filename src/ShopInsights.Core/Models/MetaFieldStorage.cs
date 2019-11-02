using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class MetaFieldStorage : ShopifyStorage<MetaField>, IMetaFieldStorage
    {
        public MetaFieldStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, c =>c .CreatedAt, c=> c.UpdatedAt)
        {
        }
    }
    public class LocationStorage : ShopifyStorage<Location>, ILocationStorage
    {
        public LocationStorage(IOptions<ShopInstanceOptions> optionsAccessor) : base(optionsAccessor, c =>c .CreatedAt, c=> c.UpdatedAt)
        {
        }
    }
}
