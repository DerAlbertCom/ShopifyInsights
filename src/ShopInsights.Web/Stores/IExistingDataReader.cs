using System.Threading;
using System.Threading.Tasks;

namespace ShopInsights.Web.Stores
{
    public interface IExistingDataReader
    {
        Task ReadExistingAsync(CancellationToken stoppingToken);
    }
}
