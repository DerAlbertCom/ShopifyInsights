using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Models
{
    public interface IShopifyDictionary<TKey, TShopify> : IEnumerable<KeyValuePair<TKey, TShopify>> where TKey : struct where TShopify : ShopifyObject
    {
        void Add(TShopify item);
        void Update(TShopify newItem);
        bool TryGetValue(TKey key, out TShopify value);
        int Count { get; }
        IEnumerable<TKey> Keys { get; }
        IEnumerable<TShopify> Values { get; }
        TShopify this[TKey key] { get; set; }
    }
}