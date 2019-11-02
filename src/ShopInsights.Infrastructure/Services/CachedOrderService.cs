using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShopifySharp;
using ShopifySharp.Filters;
using ShopInsights.Core.Services;
using ShopInsights.Core.Services.Shopify;

namespace ShopInsights.Infrastructure.Services
{
    internal class CachedOrderService : ICachedOrderService
    {
        private readonly IShopifyFactory _shopifyFactory;
        private readonly IHostEnvironment _environment;
        private readonly IStore _store;
        private readonly IFileProvider _fileProvider;

        public CachedOrderService(IShopifyFactory shopifyFactory, IHostEnvironment environment, IStore store)
        {
            _shopifyFactory = shopifyFactory;
            _environment = environment;
            _store = store;
            _fileProvider = environment.ContentRootFileProvider;
        }

        public  async Task<IEnumerable<Order>> ListAsync()
        {
            var jsonFile = _fileProvider.GetFileInfo("orders.json");
            if (jsonFile.Exists)
            {
                var o =  ReadJson(jsonFile);
                return o;
                await _store.StoreOrders(o);
            }
            var filter = new OrderFilter()
            {
                Status = "any",
                FulfillmentStatus = "any",
                FinancialStatus = "any",
                Order = "created_at asc"
            };
            var orderService = _shopifyFactory.CreateOrderService();
            var orders = new List<Order>();
            IEnumerable<Order> loadedOrders;
            do
            {
                filter.SinceId = orders.Max(o => o.Id);
                loadedOrders = await orderService.ListAsync(filter);
                orders.AddRange(loadedOrders);
            } while (loadedOrders.Any());

            WriteJson(orders, jsonFile);
            return orders;
        }

        private IEnumerable<Order> ReadJson(IFileInfo jsonFile)
        {
            using (var sr = new StreamReader(jsonFile.CreateReadStream()))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<Order[]>(reader);
            }
        }

        private void WriteJson(IEnumerable<Order> orders, IFileInfo fileInfo)
        {

            using (var streamWriter = new StreamWriter(fileInfo.PhysicalPath))
            using (var writer = new JsonTextWriter(streamWriter))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, orders);
            }
        }
    }
}
