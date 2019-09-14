using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ShopifySharp;
using ShopInsights.Core.Services;

namespace ShopInsights.Core
{

    public class OrderDictionary : IDictionary<int,Order>
    {
        readonly IDictionary<int, Order> _orders = new ConcurrentDictionary<int, Order>();

        readonly OrderUpdater _orderUpdater = new OrderUpdater();
        void ICollection<KeyValuePair<int, Order>>.Add(KeyValuePair<int, Order> item)
        {
            _orders.Add(item);
        }

        public void Clear()
        {
            _orders.Clear();
        }

        bool ICollection<KeyValuePair<int, Order>>.Contains(KeyValuePair<int, Order> item)
        {
            return _orders.Contains(item);
        }

        void ICollection<KeyValuePair<int, Order>>.CopyTo(KeyValuePair<int, Order>[] array, int arrayIndex)
        {
            _orders.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<int, Order>>.Remove(KeyValuePair<int, Order> item)
        {
            return _orders.Remove(item);
        }

        public void Add(int key, Order value)
        {
            _orders.Add(key,value);
        }

        public bool ContainsKey(int key)
        {
            return _orders.ContainsKey(key);
        }
        public bool Remove(int key)
        {
            return _orders.Remove(key);
        }

        public bool TryGetValue(int key, out Order value)
        {
            return _orders.TryGetValue(key, out value);
        }

        public int Count => _orders.Count;

        public bool IsReadOnly => false;

        public Order this[int key]
        {
            get => _orders[key];
            set => _orders[key] = value;
        }

        public ICollection<int> Keys => _orders.Keys;
        public ICollection<Order> Values => _orders.Values;

        IEnumerator<KeyValuePair<int, Order>> IEnumerable<KeyValuePair<int, Order>>.GetEnumerator()
        {
            return _orders.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _orders.GetEnumerator();
        }

        public void AddOrUpdate(Order order)
        {
            _orderUpdater.AddOrUpdate(this, order);
        }
    }
}
