using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Web.Stores
{
    public class ExistingShopifyDataReader : IExistingShopifyDataReader
    {
        public ExistingShopifyDataReader(
            IShopifyOrderFilesReader shopifyOrderReader,
            IShopifyProductFilesReader shopifyProductReader,
            IShopifyCustomerFilesReader shopifyCustomerReader,
            IShopifyMetaFieldFilesReader shopifyMetaFieldReader,
            IShopifyLocationFilesReader shopifyLocationReader,
            IOptions<StoreOptions> optionsAccessor,
            ILogger<ExistingShopifyDataReader> logger)
        {
            _shopifyOrderReader = shopifyOrderReader;
            _shopifyProductReader = shopifyProductReader;
            _shopifyCustomerReader = shopifyCustomerReader;
            _shopifyMetaFieldReader = shopifyMetaFieldReader;
            _shopifyLocationReader = shopifyLocationReader;
            _logger = logger;
            _optionsAccessor = optionsAccessor;
        }

        public async Task ReadExistingAsync(CancellationToken stoppingToken)
        {
            var filePath = _optionsAccessor.Value.GetFilePath();
            var orderPath = Path.Combine(filePath, "orders");
            EnsurePath(orderPath);
            _logger.LogDebug("Import from {path}", orderPath);
            await _shopifyOrderReader.ImportExistingAsync(orderPath, stoppingToken);

            var productPath = Path.Combine(filePath, "products");
            EnsurePath(productPath);
            _logger.LogDebug("Import from {path}", productPath);
            await _shopifyProductReader.ImportExistingAsync(productPath, stoppingToken);

            var customerPath = Path.Combine(filePath, "customers");
            EnsurePath(customerPath);
            _logger.LogDebug("Import from {path}", customerPath);
            await _shopifyCustomerReader.ImportExistingAsync(customerPath, stoppingToken);

            var metaFieldPath = Path.Combine(filePath, "metafields");
            EnsurePath(metaFieldPath);
            _logger.LogDebug("Import from {path}", metaFieldPath);
            await _shopifyMetaFieldReader.ImportExistingAsync(metaFieldPath, stoppingToken);

            var locationPath = Path.Combine(filePath, "locations");
            EnsurePath(locationPath);
            _logger.LogDebug("Import from {path}", locationPath);
            await _shopifyLocationReader.ImportExistingAsync(locationPath, stoppingToken);
        }


        void EnsurePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        readonly IShopifyOrderFilesReader _shopifyOrderReader;
        readonly IShopifyProductFilesReader _shopifyProductReader;
        readonly IShopifyCustomerFilesReader _shopifyCustomerReader;
        readonly IShopifyMetaFieldFilesReader _shopifyMetaFieldReader;
        readonly IShopifyLocationFilesReader _shopifyLocationReader;
        readonly ILogger<ExistingShopifyDataReader> _logger;
        readonly IOptions<StoreOptions> _optionsAccessor;
    }
}
