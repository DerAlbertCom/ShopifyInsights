using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyMetaFieldFilesReader : ShopifyFilesReader<MetaField>, IShopifyMetaFieldFilesReader
    {
        public ShopifyMetaFieldFilesReader(IShopifyMetaFieldStorage storage, ILogger<ShopifyMetaFieldFilesReader> logger):base(storage,"metafields", logger)
        {

        }
    }
    public class LocationShopifyFilesReader : ShopifyFilesReader<Location>, ILocationShopifyFilesReader
    {
        public LocationShopifyFilesReader(ILocationStorage storage, ILogger<LocationShopifyFilesReader> logger):base(storage,"locations", logger)
        {

        }
    }
}
