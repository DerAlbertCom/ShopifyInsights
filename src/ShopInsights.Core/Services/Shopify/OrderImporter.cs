﻿using System;
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


        public async Task<IReadOnlyCollection<Order>> GetSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogDebug("Importing Orders from Shopify since {dateTime}", sinceDate);
            var orderService = _shopifyFactory.CreateOrderService();

            var orders = new Dictionary<long,Order>();

            IReadOnlyCollection<Order> loadedOrders;

            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Order>();
                }

                loadedOrders = await orderService.ListUpdatedSinceAsync(sinceDate);
                _logger.LogInformation("Fetched {count} orders", loadedOrders.Count);
                if (orders.AddUnique(loadedOrders))
                {
                    var maxUpdates = loadedOrders.Max(o => o.UpdatedAt);
                    _logger.LogDebug("Fetching rest of orders from Shopify since {dateTime}", maxUpdates);
                    if (maxUpdates.HasValue)
                    {
                        sinceDate = maxUpdates.Value;
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
