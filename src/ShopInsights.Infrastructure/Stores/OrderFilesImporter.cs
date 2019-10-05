using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class OrderFilesImporter : IOrderFilesImporter
    {
        public OrderFilesImporter(IOrderStorage orderStorage, IOptions<OrderStoreOptions> optionsAccessor,
            ILogger<OrderFilesImporter> logger)
        {
            _orderStorage = orderStorage;
            _optionsAccessor = optionsAccessor;
            _logger = logger;
        }

        public Task ImportExistingOrdersAsync(CancellationToken stoppingToken)
        {
            var importPath = _optionsAccessor.Value.ImportPath;

            _logger.LogInformation("Importing Order Files from {Path}", importPath);

            var fileProvider = new PhysicalFileProvider(importPath);

            var files = fileProvider.GetDirectoryContents("./").Where(IsOrderFile).ToArray();
            var serializer = JsonSerializer.Create();

            var orderCount = 0;
            foreach (var file in files)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Importing of orders stopped because of stopping application");
                    break;
                }

                using (var streamReader = new StreamReader(file.CreateReadStream()))
                {
                    var jsonReader = new JsonTextReader(streamReader);
                    var existingOrders = serializer.Deserialize<Order[]>(jsonReader);
                    orderCount += existingOrders.Length;
                    UpdateOrders(existingOrders);
                }
            }

            _logger.LogInformation("Imported up to {orderCount} orders", orderCount);

            return Task.CompletedTask;
        }

        private void UpdateOrders(Order[] existingOrders)
        {
            _orderStorage.AddOrders(existingOrders);
        }

        private bool IsOrderFile(IFileInfo fileInfo)
        {
            if (fileInfo.IsDirectory)
            {
                return false;
            }

            if (!fileInfo.Name.StartsWith("orders", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return (fileInfo.Name.EndsWith(".json", StringComparison.OrdinalIgnoreCase));
        }

        private readonly IOrderStorage _orderStorage;
        private readonly IOptions<OrderStoreOptions> _optionsAccessor;
        private readonly ILogger<OrderFilesImporter> _logger;
    }
}
