using System;
using ShopifySharp;

namespace ShopInsights.Web.Controllers
{
    internal static class OrderFilters
    {
        public static bool NotCancelledOrder(Order order)
        {
            return !string.Equals(order.FinancialStatus, "voided", StringComparison.OrdinalIgnoreCase);
        }

        public static bool OnlineOrder(Order order)
        {
            if (!order.LocationId.HasValue)
            {
                return NotCancelledOrder(order);
            }

            return false;
        }

        public static bool OrderOnLocation(Order order, long locationId)
        {
            if (order.LocationId.HasValue && order.LocationId.Value == locationId)
            {
                return NotCancelledOrder(order);
            }

            return false;
        }
    }
}
