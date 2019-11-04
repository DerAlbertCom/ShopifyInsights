using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Stores
{
    public interface IShopifyFilesReader<T> where T: ShopifyObject
    {
        Task ImportExistingAsync(string path, CancellationToken stoppingToken);
    }
}
