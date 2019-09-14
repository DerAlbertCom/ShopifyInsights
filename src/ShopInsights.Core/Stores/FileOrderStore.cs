using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShopifySharp;

namespace ShopInsights.Core.Stores
{
    public class FileOrderStore
    {
        readonly IOptionsSnapshot<OrderStoreOptions> _optionsAccessor;

        public FileOrderStore(IOptionsSnapshot<OrderStoreOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        public Task<OrderDictionary> ImportExistingOrdersAsync()
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
                    UpdateOrders(orders, existingOrders);
                }
            }

            return Task.FromResult(orders);
        }

        void UpdateOrders(OrderDictionary orders, Order[] existingOrders)
        {
            foreach (var existingOrder in existingOrders)
            {
                orders.AddOrUpdate(existingOrder);
            }
        }

        bool IsOrderFile(IFileInfo fileInfo)
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
    }
}
