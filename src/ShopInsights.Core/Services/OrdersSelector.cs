using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;
using ShopInsights.Core.Models;

namespace ShopInsights.Core.Services
{
    internal class OrdersSelector : IOrdersSelector
    {
        private readonly TimeZoneInfo _timeZone;

        public OrdersSelector(IOptionsSnapshot<ShopInstanceOptions> optionsAccessor)
        {
            _timeZone = optionsAccessor.Value.TimeZoneInfo;
        }

        public Order[] SelectForDate(OrderDictionary dictionary, DateTime dateTime)
        {
            var date = _timeZone.GetTimeZoneCorrectedDate(dateTime);

            var selectedOrders = SelectOrdersForDate(dictionary, date).ToArray();
            return selectedOrders;
        }

        private IEnumerable<Order> SelectOrdersForDate(OrderDictionary dictionary, DateTime date)
        {
            bool TimeZoneAwareDateFilter(Order order)
            {
                if (!order.CreatedAt.HasValue)
                {
                    return false;
                }

                var orderDate = _timeZone.GetTimeZoneCorrectedDate(order.CreatedAt.Value);
                return orderDate == date;
            }

            return SelectOrders(dictionary, TimeZoneAwareDateFilter);
        }

        private IEnumerable<Order> SelectOrders(OrderDictionary dictionary, Func<Order, bool> predicate)
        {
            return dictionary.Values.Where(predicate).OrderBy(o => o.OrderNumber);
        }

    }
}
