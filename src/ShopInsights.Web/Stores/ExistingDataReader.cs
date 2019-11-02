﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopInsights.Core.Stores;

namespace ShopInsights.Web.Stores
{
    public class ExistingDataReader : IExistingDataReader
    {
        public ExistingDataReader(
            IOrderFilesReader orderReader,
            IProductFilesReader productReader,
            ICustomerFilesReader customerReader,
            IMetaFieldFilesReader metaFieldReader,
            ILocationFilesReader locationReader,
            IOptions<StoreOptions> optionsAccessor,
            ILogger<ExistingDataReader> logger)
        {
            _orderReader = orderReader;
            _productReader = productReader;
            _customerReader = customerReader;
            _metaFieldReader = metaFieldReader;
            _locationReader = locationReader;
            _logger = logger;
            _optionsAccessor = optionsAccessor;
        }

        public async Task ReadExistingAsync(CancellationToken stoppingToken)
        {
            var filePath = _optionsAccessor.Value.FilePath;
            var orderPath = Path.Combine(filePath, "orders");
            EnsurePath(orderPath);
            _logger.LogDebug("Import from {path}", orderPath);
            await _orderReader.ImportExistingAsync(orderPath, stoppingToken);

            var productPath = Path.Combine(filePath, "products");
            EnsurePath(productPath);
            _logger.LogDebug("Import from {path}", productPath);
            await _productReader.ImportExistingAsync(productPath, stoppingToken);

            var customerPath = Path.Combine(filePath, "customers");
            EnsurePath(customerPath);
            _logger.LogDebug("Import from {path}", customerPath);
            await _customerReader.ImportExistingAsync(customerPath, stoppingToken);

            var metaFieldPath = Path.Combine(filePath, "metafields");
            EnsurePath(metaFieldPath);
            _logger.LogDebug("Import from {path}", metaFieldPath);
            await _metaFieldReader.ImportExistingAsync(metaFieldPath, stoppingToken);

            var locationPath = Path.Combine(filePath, "locations");
            EnsurePath(locationPath);
            _logger.LogDebug("Import from {path}", locationPath);
            await _locationReader.ImportExistingAsync(locationPath, stoppingToken);
        }

        private void EnsurePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private readonly IOrderFilesReader _orderReader;
        private readonly IProductFilesReader _productReader;
        private readonly ICustomerFilesReader _customerReader;
        private readonly IMetaFieldFilesReader _metaFieldReader;
        private readonly ILocationFilesReader _locationReader;
        private readonly ILogger<ExistingDataReader> _logger;
        private readonly IOptions<StoreOptions> _optionsAccessor;
    }
}
