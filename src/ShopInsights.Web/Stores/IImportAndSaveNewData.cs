using System.Threading;
using System.Threading.Tasks;

namespace ShopInsights.Web.Stores
{
    public interface IImportAndSaveNewData
    {
        Task ImportAndSaveAsync(CancellationToken stoppingToken);
    }
}