using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Models
{
    public  class ShopifyDictionary<TKey, TShopify> : IShopifyDictionary<TKey, TShopify> where TKey : struct
        where TShopify : ShopifyObject
    {
        public ShopifyDictionary(Func<TShopify, TKey?> keyKeySelector)
        {
            _keySelector = keyKeySelector;
        }

        private readonly Func<TShopify, TKey?> _keySelector;
        public void Add(TShopify item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var key = _keySelector(item);
            if (!key.HasValue)
            {
                throw new ArgumentOutOfRangeException($"This {typeof(TShopify).Name} is missing an the key");
            }

            _items.Add(key.Value, item);
        }

        public void Update(TShopify newItem)
        {
            var key = _keySelector(newItem);
            if (!key.HasValue)
            {
                throw new ArgumentOutOfRangeException($"This order is missing an {typeof(TShopify).Name} key");
            }

            if (!_items.ContainsKey(key.Value))
            {
                throw new InvalidOperationException($"You are updating a not existing {typeof(TShopify).Name} with the key {key.Value}");
            }

            _items[key.Value] = newItem;
        }

        public bool TryGetValue(TKey key, out TShopify value)
        {
            return _items.TryGetValue(key, out value);
        }

        public int Count => _items.Count;

        public TShopify this[TKey key]
        {
            get => _items[key];
            set => _items[key] = value;
        }

        public IEnumerable<TKey> Keys => _items.Keys;

        public IEnumerable<TShopify> Values => _items.Values;

        IEnumerator<KeyValuePair<TKey, TShopify>> IEnumerable<KeyValuePair<TKey, TShopify>>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        private readonly IDictionary<TKey, TShopify> _items = new ConcurrentDictionary<TKey, TShopify>();
    }
}
