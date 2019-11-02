using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Services.Shopify
{
    public static class ShopifyExtensions
    {
        public static bool AddUnique<T>(this IDictionary<long, T> objects, IReadOnlyCollection<T> freshObjects) where T : ShopifyObject
        {
            var added = false;
            foreach (var shopifyObject in freshObjects)
            {
                if (!shopifyObject.Id.HasValue) continue;

                if (!objects.ContainsKey(shopifyObject.Id.Value))
                {
                    objects[shopifyObject.Id.Value] = shopifyObject;
                    added = true;
                }
            }

            return added;
        }
    }
}
