using ShopifySharp;

namespace ShopInsights.Core.Stores
{
    public interface IShopifyMetaFieldFilesReader : IShopifyFilesReader<MetaField>
    {
    }
    public interface ILocationShopifyFilesReader : IShopifyFilesReader<Location>
    {
    }
}
