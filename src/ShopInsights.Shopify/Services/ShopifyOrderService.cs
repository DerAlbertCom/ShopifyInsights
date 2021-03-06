﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Shopify.Services
{
    internal class ShopifyOrderService : IShopifyOrderService
    {
        readonly OrderService _orderService;

        public ShopifyOrderService(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IReadOnlyCollection<Order>> ListUpdatedSinceAsync(DateTimeOffset sinceDate)
        {
            var filter = new OrderFilter()
            {
                Order = "updated_at asc",
                Limit = 200,
                FulfillmentStatus = "any",
                FinancialStatus = "any",
                Status = "any",

                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            var orders = await _orderService.ListAsync(filter);
            return orders.ToArray();
        }
    }
}
