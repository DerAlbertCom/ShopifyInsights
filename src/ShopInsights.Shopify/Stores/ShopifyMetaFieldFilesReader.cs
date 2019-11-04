using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyMetaFieldFilesReader : ShopifyFilesReader<MetaField>, IShopifyMetaFieldFilesReader
    {
        public ShopifyMetaFieldFilesReader(IShopifyMetaFieldStorage storage, ILogger<ShopifyMetaFieldFilesReader> logger):base(storage,"metafields", logger)
        {

        }
    }
}
