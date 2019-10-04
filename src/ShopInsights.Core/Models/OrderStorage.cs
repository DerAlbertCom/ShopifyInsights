using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Configuration;

namespace ShopInsights.Core.Models
{
    public class OrderStorage : IOrderStorage
    {
        public OrderStorage(IOptions<ShopInstanceOptions> optionsAccessor)
        {
            _timeZoneInfo = optionsAccessor.Value.TimeZoneInfo;
        }

        public Order[] GetOrdersForDate(DateTime date)
        {
            date = _timeZoneInfo.GetTimeZoneCorrectedDate(date);
            if (_dictionary.TryGetValue(date, out var orders))
            {
                return orders.Values.OrderBy(o => o.OrderNumber.Value).ToArray();
            }

            return Array.Empty<Order>();
        }

        public IEnumerable<DateTime> ModifiedDates
        {
            get { return _modifiedDates.OrderBy(d => d); }
        }

        public IEnumerable<Order> AllOrders
        {
            get
            {
                var dates = _dictionary.Keys.OrderBy(d => d);
                foreach (var date in dates)
                {
                    var orders = GetOrderDictionary(date).Values.OrderBy(o => o.OrderNumber);
                    foreach (var order in orders)
                    {
                        yield return order;
                    }
                }
            }
        }

        public void ResetModifiedDates()
        {
            _modifiedDates.Clear();
        }

        public void AddOrders(IEnumerable<Order> newOrders)
        {
            foreach (var newOrder in newOrders)
            {
                if (!newOrder.CreatedAt.HasValue)
                {
                    continue;
                }

                var orderDictionary = GetOrderDictionary(newOrder.CreatedAt.Value);
                if (orderDictionary.TryGetValue(newOrder.OrderNumber.Value, out var existingOrder))
                {
                    if (!existingOrder.UpdatedAt.HasValue)
                    {
                        orderDictionary.Update(newOrder);
                        UpdateModifiedDate(newOrder);
                    }
                    else if (newOrder.UpdatedAt.Value > existingOrder.UpdatedAt.Value)
                    {
                        orderDictionary.Update(newOrder);
                        UpdateModifiedDate(newOrder);
                    }
                }
                else
                {
                    orderDictionary.Add(newOrder);
                }
            }
        }

        private void UpdateModifiedDate(Order newOrder)
        {
            var date = _timeZoneInfo.GetTimeZoneCorrectedDate(newOrder.CreatedAt.Value);
            _modifiedDates.Add(date);
        }

        private OrderDictionary GetOrderDictionary(DateTimeOffset orderCreatedAt)
        {
            var date = _timeZoneInfo.GetTimeZoneCorrectedDate(orderCreatedAt);

            if (_dictionary.TryGetValue(date, out var orders))
            {
                return orders;
            }

            _dictionary[date] = new OrderDictionary();
            return _dictionary[date];
        }

        private readonly Dictionary<DateTime, OrderDictionary>
            _dictionary = new Dictionary<DateTime, OrderDictionary>();

        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly HashSet<DateTime> _modifiedDates = new HashSet<DateTime>();
    }
}
