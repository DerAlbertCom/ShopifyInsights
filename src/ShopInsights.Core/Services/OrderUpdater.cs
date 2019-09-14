using System;
using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Services
{
    internal class OrderUpdater
    {
        public void AddOrUpdate(OrderDictionary orders, Order order)
        {
            if (!order.OrderNumber.HasValue)
            {
                return;
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

        void ModifiedOrder(Order order)
        {
            if (!order.CreatedAt.HasValue)
            {
                return;
            }

            var date = order.CreatedAt.Value.Date;

            if (ModifiedDates.Contains(date))
            {
                return;
            }

            ModifiedDates.Add(date);
        }

        public ISet<DateTime> ModifiedDates { get; } = new HashSet<DateTime>();
    }
}
