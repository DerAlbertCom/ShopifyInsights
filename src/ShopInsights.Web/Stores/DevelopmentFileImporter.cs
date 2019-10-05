using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ShopInsights.Core.Stores;

namespace ShopInsights.Web.Stores
{
    public class DevelopmentFileImporter : BackgroundService
    {
        public DevelopmentFileImporter(IOrderFilesImporter importer)
        {
            _importer = importer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                await _importer.ImportExistingOrdersAsync(stoppingToken);

            }, stoppingToken);
        }

        private readonly IOrderFilesImporter _importer;
    }
}
