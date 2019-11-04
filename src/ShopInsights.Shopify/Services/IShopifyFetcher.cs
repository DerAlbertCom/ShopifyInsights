using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    public interface IShopifyFetcher<T> where T : ShopifyObject
    {
        Task<IReadOnlyCollection<T>> GetUpdatedSinceAsync(DateTimeOffset sinceDate, CancellationToken stoppingToken);
    }
}
