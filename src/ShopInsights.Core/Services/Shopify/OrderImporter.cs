using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Core.Services.Shopify
{
    class OrderImporter : IOrderImporter
    {
        private readonly IShopifyFactory _shopifyFactory;
        private readonly ILogger<OrderImporter> _logger;

        public OrderImporter(IShopifyFactory shopifyFactory, ILogger<OrderImporter> logger)
        {
            _shopifyFactory = shopifyFactory;
            _logger = logger;
        }
        public async Task<IReadOnlyCollection<Order>> GetOrderSinceId(long id)
        {
            var orderService = _shopifyFactory.CreateOrderService();
            var orders = new Dictionary<long,Order>();

            IReadOnlyCollection<Order> loadedOrders;

            var filter = new OrderFilter()
            {
                Status = "any",
                FulfillmentStatus = "any",
                FinancialStatus = "any",
                Order = "id asc",
                SinceId = id
            };
            do
            {

                loadedOrders = (await orderService.ListAsync(filter)).ToArray();
                orders.AddUnique(loadedOrders);

                var maxId = loadedOrders.Max(o => o.Id);
                if (maxId.HasValue)
                {

                    filter.SinceId = id;
                }
                else
                {
                    break;
                }

            } while (loadedOrders.Any());

            return orders.Values;
        }

        public async Task<IReadOnlyCollection<Order>> GetSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogDebug("Importing Orders from Shopify since {dateTime}", sinceDate);
            var orderService = _shopifyFactory.CreateOrderService();

            var orders = new Dictionary<long,Order>();

            IReadOnlyCollection<Order> loadedOrders;

            var filter = new OrderFilter()
            {
                Status = "any",
                FulfillmentStatus = "any",
                FinancialStatus = "any",
                Order = "updated_at asc",
                Limit = 200,
                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Order>();
                }

                loadedOrders = (await orderService.ListAsync(filter)).ToArray();
                _logger.LogInformation("Fetched {count} orders", loadedOrders.Count);
                if (orders.AddUnique(loadedOrders))
                {
                    var maxUpdates = loadedOrders.Max(o => o.UpdatedAt);
                    _logger.LogDebug("Fetching rest of orders from Shopify since {dateTime}", maxUpdates);
                    if (maxUpdates.HasValue)
                    {
                        filter.UpdatedAtMin = maxUpdates.Value.Subtract(TimeSpan.FromSeconds(1));
                    }
                    else
                    {
                        break;
                    }

                }
                else
                {
                    break;
                }


            } while (loadedOrders.Any());

            return orders.Values;
        }


    }
}
