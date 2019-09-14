using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Services
{
    public class OrderUpdater
    {
        public void Update(Dictionary<int, Order> orders, Order order)
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
        }
    }
}
