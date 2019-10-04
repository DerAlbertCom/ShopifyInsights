using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Models
{
    public class OrderDictionary : IEnumerable<KeyValuePair<int, Order>>
    {
        public void Add(Order item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!item.OrderNumber.HasValue)
            {
                throw new ArgumentOutOfRangeException($"This order is missing an order number");
            }

            if (!item.CreatedAt.HasValue)
            {
                throw new ArgumentOutOfRangeException($"CreatedAt is missing on order number {item.OrderNumber}");
            }

            _orders.Add(item.OrderNumber.Value, item);
        }

        public void Update(Order newOrder)
        {
            if (!newOrder.OrderNumber.HasValue)
            {
                throw new ArgumentOutOfRangeException($"This order is missing an order number");
            }

            if (!_orders.ContainsKey(newOrder.OrderNumber.Value))
            {
                throw new InvalidOperationException($"You are updating a not existing Order with the OrderNumber {newOrder.OrderNumber.Value}");
            }

            _orders[newOrder.OrderNumber.Value] = newOrder;
        }

        public bool TryGetValue(int key, out Order value)
        {
            return _orders.TryGetValue(key, out value);
        }

        public int Count => _orders.Count;

        public Order this[int key]
        {
            get => _orders[key];
            set => _orders[key] = value;
        }

        public IEnumerable<int> Keys => _orders.Keys;

        public IEnumerable<Order> Values => _orders.Values;

        IEnumerator<KeyValuePair<int, Order>> IEnumerable<KeyValuePair<int, Order>>.GetEnumerator()
        {
            return _orders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _orders.GetEnumerator();
        }

        private readonly IDictionary<int, Order> _orders = new ConcurrentDictionary<int, Order>();
    }
}
