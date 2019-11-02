using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShopInsights.Web.Stores
{
    public class DataInitialization : BackgroundService
    {
        private readonly IExistingDataReader _reader;
        private readonly IFetchAndStoreUpdatedDataService _storeUpdatedDataService;
        private readonly ILogger<DataInitialization> _logger;

        public DataInitialization(IExistingDataReader reader, IFetchAndStoreUpdatedDataService storeUpdatedDataService, ILogger<DataInitialization> logger)
        {
            _reader = reader;
            _storeUpdatedDataService = storeUpdatedDataService;
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
                    await _storeUpdatedDataService.FetchAndStoreAsync(stoppingToken);

                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Exception on DataInitialization");
                }
            }, stoppingToken);
        }
    }
}
