using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopInsights.Core.Services.Loaders;

namespace ShopInsights.Web.Stores
{
    public class ImportAndSaveNewData : IImportAndSaveNewData
    {
        private readonly IProductLoader _productLoader;
        private readonly ICustomerLoader _customerLoader;
        private readonly IOrderLoader _orderLoader;
        private readonly ILogger<ImportAndSaveNewData> _logger;

        public ImportAndSaveNewData(IProductLoader productLoader, ICustomerLoader customerLoader, IOrderLoader orderLoader, ILogger<ImportAndSaveNewData> logger)
        {
            _productLoader = productLoader;
            _customerLoader = customerLoader;
            _orderLoader = orderLoader;
            _logger = logger;
        }

        public async Task ImportAndSaveAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Load new Products");
            await _productLoader.LoadNewAndSaveChangesAsync(stoppingToken);
            _logger.LogDebug("Load new Customers");
            await _customerLoader.LoadNewAndSaveChangesAsync(stoppingToken);
            _logger.LogDebug("Load new Orders");
            await _orderLoader.LoadNewAndSaveChangesAsync(stoppingToken);
        }
    }
}
