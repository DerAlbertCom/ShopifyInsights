using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShopInsights.Web.Stores
{
    public class DataInitialization : BackgroundService
    {
        private readonly IExistingDataImporter _importer;
        private readonly IImportAndSaveNewData _saveNewData;
        private readonly ILogger<DataInitialization> _logger;

        public DataInitialization(IExistingDataImporter importer, IImportAndSaveNewData saveNewData, ILogger<DataInitialization> logger)
        {
            _importer = importer;
            _saveNewData = saveNewData;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                try
                {
                    _logger.LogInformation("Import Existing");
                    await _importer.ImportExistingAsync(stoppingToken);
                    if (stoppingToken.IsCancellationRequested)
                    {
                        return;
                    }

                    _logger.LogInformation("Import new and Save");
                    await _saveNewData.ImportAndSaveAsync(stoppingToken);

                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Exception on DataInitialization");
                }
            }, stoppingToken);
        }
    }
}
