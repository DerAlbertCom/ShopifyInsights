using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopInsights.Core.Stores;

namespace ShopInsights.Web.Stores
{
    public class ExistingDataImporter : IExistingDataImporter
    {
        public ExistingDataImporter(IOrderFilesImporter orderImporter, IProductFilesImporter productImporter,
            ICustomerFilesImporter customerImporter, IOptions<StoreOptions> optionsAccessor, ILogger<ExistingDataImporter> logger)
        {
            _orderImporter = orderImporter;
            _productImporter = productImporter;
            _customerImporter = customerImporter;
            _logger = logger;
            _optionsAccessor = optionsAccessor;
        }

        public async Task ImportExistingAsync(CancellationToken stoppingToken)
        {
            var filePath = _optionsAccessor.Value.FilePath;
            var orderPath = Path.Combine(filePath, "orders");
            EnsurePath(orderPath);
            _logger.LogDebug("Import from {path}", orderPath);
            await _orderImporter.ImportExistingAsync(orderPath, stoppingToken);

            var productPath = Path.Combine(filePath, "products");
            EnsurePath(productPath);
            _logger.LogDebug("Import from {path}", productPath);
            await _productImporter.ImportExistingAsync(productPath, stoppingToken);

            var customerPath = Path.Combine(filePath, "customers");
            EnsurePath(customerPath);
            _logger.LogDebug("Import from {path}", customerPath);
            await _customerImporter.ImportExistingAsync(customerPath, stoppingToken);
        }

        private void EnsurePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private readonly IOrderFilesImporter _orderImporter;
        private readonly IProductFilesImporter _productImporter;
        private readonly ICustomerFilesImporter _customerImporter;
        private readonly ILogger<ExistingDataImporter> _logger;
        private readonly IOptions<StoreOptions> _optionsAccessor;
    }
}
