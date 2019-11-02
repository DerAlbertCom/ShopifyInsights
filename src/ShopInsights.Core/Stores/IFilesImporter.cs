﻿using System.Threading;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Core.Stores
{
    public interface IFilesImporter<T> where T: ShopifyObject
    {
        Task ImportExistingAsync(string path, CancellationToken stoppingToken);
    }
}
