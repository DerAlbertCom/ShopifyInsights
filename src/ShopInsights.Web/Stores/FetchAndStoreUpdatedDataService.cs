using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopInsights.Core.Services.FetchAndStore;

namespace ShopInsights.Web.Stores
{
    public class FetchAndStoreUpdatedDataService : IFetchAndStoreUpdatedDataService
    {
        private readonly IProductFetchAndStoreService _productFetchAndStoreService;
        private readonly ICustomerFetchAndStoreService _customerFetchAndStoreService;
        private readonly IOrderFetchAndStoreService _orderFetchAndStoreService;
        private readonly ILogger<FetchAndStoreUpdatedDataService> _logger;

        public FetchAndStoreUpdatedDataService(IProductFetchAndStoreService productFetchAndStoreService, ICustomerFetchAndStoreService customerFetchAndStoreService, IOrderFetchAndStoreService orderFetchAndStoreService, ILogger<FetchAndStoreUpdatedDataService> logger)
        {
            _productFetchAndStoreService = productFetchAndStoreService;
            _customerFetchAndStoreService = customerFetchAndStoreService;
            _orderFetchAndStoreService = orderFetchAndStoreService;
            _logger = logger;
        }

        public async Task FetchAndStoreAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Load new Products");
            await _productFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
            _logger.LogDebug("Load new Customers");
            await _customerFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
            _logger.LogDebug("Load new Orders");
            await _orderFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
        }
    }
}
