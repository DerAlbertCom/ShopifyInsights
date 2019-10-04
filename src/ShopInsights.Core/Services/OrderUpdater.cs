using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;
using ShopInsights.Core.Models;

namespace ShopInsights.Core.Services
{
    internal class OrderUpdater : IOrderUpdater
    {
        private readonly TimeZoneInfo _timeZone;

        public OrderUpdater(IOptionsSnapshot<ShopInstanceOptions> optionsAccessor)
        {
            _timeZone = optionsAccessor.Value.TimeZoneInfo;
        }

        public void AddOrUpdate(OrderDictionary orders, Order order)
        {
            if (!order.OrderNumber.HasValue)
            {
                throw new ArgumentOutOfRangeException($"there is no OrderNumber");
            }

            var orderNumber = order.OrderNumber.Value;
            if (orders.TryGetValue(orderNumber, out var existingOrder))
            {
                if (existingOrder.UpdatedAt > order.UpdatedAt)
                {
                    return;
                }
            }

            orders[order.OrderNumber.Value] = order;
            ModifiedOrder(order);
        }

        private void ModifiedOrder(Order order)
        {
            if (!order.CreatedAt.HasValue)
            {
                return;
            }

            var orderDate = _timeZone.GetTimeZoneCorrectedDate(order.CreatedAt.Value);

            if (ModifiedDates.Contains(orderDate))
            {
                return;
            }

            ModifiedDates.Add(orderDate);
        }


        public ISet<DateTime> ModifiedDates { get; } = new HashSet<DateTime>();
    }
}
