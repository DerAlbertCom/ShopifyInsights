using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyMetaFieldFileWriter : ShopifyFilesWriter<MetaField>, IShopifyMetaFieldFilesWriter
    {
        public ShopifyMetaFieldFileWriter(IShopifyMetaFieldStorage storage, ILogger<ShopifyMetaFieldFileWriter> logger) : base(storage, "metafields", logger)
        {
        }
    }
    public class LocationShopifyFileWriter : ShopifyFilesWriter<Location>, ILocationShopifyFilesWriter
    {
        public LocationShopifyFileWriter(ILocationStorage storage, ILogger<LocationShopifyFileWriter> logger) : base(storage, "locations", logger)
        {
        }
    }
}
