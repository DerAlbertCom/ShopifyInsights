using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public class ShopifyMetaFieldFileWriter : ShopifyFilesWriter<MetaField>, IShopifyMetaFieldFilesWriter
    {
        public ShopifyMetaFieldFileWriter(IShopifyMetaFieldStorage storage, ILogger<ShopifyMetaFieldFileWriter> logger) : base(storage, "metafields", logger)
        {
        }
    }
}
