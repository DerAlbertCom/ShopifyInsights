using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class OrderFilesImporter : IOrderFilesImporter
    {
        public OrderFilesImporter(IOrderStorage orderStorage, IOptionsSnapshot<OrderStoreOptions> optionsAccessor)
        {
            _orderStorage = orderStorage;
            _optionsAccessor = optionsAccessor;
        }

        public Task ImportExistingOrdersAsync()
        {
            var orders = new OrderDictionary();

            var fileProvider = new PhysicalFileProvider(_optionsAccessor.Value.ImportPath);
            var files = fileProvider.GetDirectoryContents("./").Where(IsOrderFile).ToArray();
                                                                 var serializer = JsonSerializer.Create();
            foreach (var file in files)
            {
                using (var streamReader = new StreamReader(file.CreateReadStream()))
                {
                    var jsonReader = new JsonTextReader(streamReader);
                    var existingOrders = serializer.Deserialize<Order[]>(jsonReader);
                    UpdateOrders(existingOrders);
                }
            }

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
        private readonly IOptionsSnapshot<OrderStoreOptions> _optionsAccessor;
    }
}
