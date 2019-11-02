using System.Threading;
using System.Threading.Tasks;

namespace ShopInsights.Web.Stores
{
    public interface IFetchAndStoreUpdatedDataService
    {
        Task FetchAndStoreAsync(CancellationToken stoppingToken);
    }
}
