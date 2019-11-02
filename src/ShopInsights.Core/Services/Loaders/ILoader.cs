using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Services.Loaders
{
    public interface ILoader<T> where T : ShopifyObject
    {
        Task LoadNewAndSaveChangesAsync(CancellationToken stoppingToken);
    }
}