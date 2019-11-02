﻿using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Stores
{
    public interface IFilesStorage<T> where T: ShopifyObject
    {
        Task StoreFilesAsync(string storePath, CancellationToken stoppingToken);
    }
}
