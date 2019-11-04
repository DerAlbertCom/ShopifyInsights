using System.Threading;
using System.Threading.Tasks;

namespace ShopInsights.Web.Stores
{
    public interface IFetchAndStoreUpdatedShopifyDataService
    {
        Task FetchAndStoreAsync(CancellationToken stoppingToken);
    }
}
