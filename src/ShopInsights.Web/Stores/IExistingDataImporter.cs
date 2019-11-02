using System.Threading;
using System.Threading.Tasks;

namespace ShopInsights.Web.Stores
{
    public interface IExistingDataImporter
    {
        Task ImportExistingAsync(CancellationToken stoppingToken);
    }
}