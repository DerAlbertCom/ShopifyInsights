using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Services.Shopify
{
    public interface IImporter<T> where T : ShopifyObject
    {
        Task<IReadOnlyCollection<T>> GetSinceAsync(DateTimeOffset sinceDate, CancellationToken stoppingToken);
    }
}
