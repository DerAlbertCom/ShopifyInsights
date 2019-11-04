using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopInsights.Shopify.Services;
using ShopInsights.Shopify.Services.FetchAndStore;

namespace ShopInsights.Web.Stores
{
    public class FetchAndStoreUpdatedShopifyDataService : IFetchAndStoreUpdatedShopifyDataService
    {
        private readonly IShopifyProductShopifyFetchAndStoreService _shopifyProductShopifyFetchAndStoreService;
        private readonly IShopifyCustomerShopifyFetchAndStoreService _shopifyCustomerShopifyFetchAndStoreService;
        private readonly IShopifyOrderShopifyFetchAndStoreService _shopifyOrderShopifyFetchAndStoreService;
        private readonly IShopifyMetaFieldShopifyFetchAndStoreService _shopifyMetaFieldShopifyFetchAndStoreService;
        private readonly ILocationShopifyFetchAndStoreService _locationShopifyFetchAndStoreService;
        private readonly IOptions<ShopifyOptions> _optionsAccessor;
        private readonly ILogger<FetchAndStoreUpdatedShopifyDataService> _logger;

        public FetchAndStoreUpdatedShopifyDataService(
            IShopifyProductShopifyFetchAndStoreService shopifyProductShopifyFetchAndStoreService,
            IShopifyCustomerShopifyFetchAndStoreService shopifyCustomerShopifyFetchAndStoreService,
            IShopifyOrderShopifyFetchAndStoreService shopifyOrderShopifyFetchAndStoreService,
            IShopifyMetaFieldShopifyFetchAndStoreService shopifyMetaFieldShopifyFetchAndStoreService,
            ILocationShopifyFetchAndStoreService locationShopifyFetchAndStoreService,
            IOptions<ShopifyOptions> optionsAccessor,
            ILogger<FetchAndStoreUpdatedShopifyDataService> logger)
        {
            _shopifyProductShopifyFetchAndStoreService = shopifyProductShopifyFetchAndStoreService;
            _shopifyCustomerShopifyFetchAndStoreService = shopifyCustomerShopifyFetchAndStoreService;
            _shopifyOrderShopifyFetchAndStoreService = shopifyOrderShopifyFetchAndStoreService;
            _shopifyMetaFieldShopifyFetchAndStoreService = shopifyMetaFieldShopifyFetchAndStoreService;
            _locationShopifyFetchAndStoreService = locationShopifyFetchAndStoreService;
            _optionsAccessor = optionsAccessor;
            _logger = logger;
        }

        public async Task FetchAndStoreAsync(CancellationToken stoppingToken)
        {
            if (!_optionsAccessor.Value.FetchNewData)
            {
                _logger.LogInformation("Not fetching new Data from Shopify, because of configuration");
                return;
            }
            _logger.LogDebug("Load new Products");
            await _shopifyProductShopifyFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
            _logger.LogDebug("Load new MetaFields");
            await _shopifyMetaFieldShopifyFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
            _logger.LogDebug("Load new Customers");
            await _shopifyCustomerShopifyFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
            _logger.LogDebug("Load new Orders");
            await _shopifyOrderShopifyFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
            _logger.LogDebug("Load new Locations");
            await _locationShopifyFetchAndStoreService.FetchUpdatesAndStoreAsync(stoppingToken);
        }
    }
}
