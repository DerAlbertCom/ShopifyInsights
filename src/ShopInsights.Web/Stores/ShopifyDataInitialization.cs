using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShopInsights.Web.Stores
{
    public class ShopifyDataInitialization : BackgroundService
    {
        readonly IExistingShopifyDataReader _reader;
        readonly IFetchAndStoreUpdatedShopifyDataService _storeUpdatedShopifyDataService;
        readonly ILogger<ShopifyDataInitialization> _logger;

        public ShopifyDataInitialization(IExistingShopifyDataReader reader, IFetchAndStoreUpdatedShopifyDataService storeUpdatedShopifyDataService, ILogger<ShopifyDataInitialization> logger)
        {
            _reader = reader;
            _storeUpdatedShopifyDataService = storeUpdatedShopifyDataService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("Import Existing");
                    await _reader.ReadExistingAsync(stoppingToken);
                    if (stoppingToken.IsCancellationRequested)
                    {
                        return;
                    }

                    _logger.LogInformation("Import new and Save");
                    await _storeUpdatedShopifyDataService.FetchAndStoreAsync(stoppingToken);

                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Exception on DataInitialization");
                }
            }, stoppingToken);
        }
    }
}
