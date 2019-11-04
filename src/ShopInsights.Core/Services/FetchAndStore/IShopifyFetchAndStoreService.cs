using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Services.FetchAndStore
{
    public interface IShopifyFetchAndStoreService<T> where T : ShopifyObject
    {
        Task FetchUpdatesAndStoreAsync(CancellationToken stoppingToken);
    }
}
