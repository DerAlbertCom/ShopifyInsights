using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services.Shopify
{
    public interface IShopifyFetcher<T> where T : ShopifyObject
    {
        Task<IReadOnlyCollection<T>> GetUpdatedSinceAsync(DateTimeOffset sinceDate, CancellationToken stoppingToken);
    }
}
