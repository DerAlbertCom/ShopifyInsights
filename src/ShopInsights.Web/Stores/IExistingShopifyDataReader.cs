using System.Threading;
using System.Threading.Tasks;

namespace ShopInsights.Web.Stores
{
    public interface IExistingShopifyDataReader
    {
        Task ReadExistingAsync(CancellationToken stoppingToken);
    }
}
