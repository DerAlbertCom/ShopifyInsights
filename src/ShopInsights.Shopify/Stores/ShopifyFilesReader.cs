﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Shopify.Stores
{
    public abstract class ShopifyFilesReader<T> : IShopifyFilesReader<T> where T : ShopifyObject
    {
        protected ShopifyFilesReader(IShopifyStorage<T> storage,
            string startFile,
            ILogger logger)
        {
            _storage = storage;
            _logger = logger;
            _startFile = startFile;
        }

        public Task ImportExistingAsync(string importPath, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Importing {type} Files from {Path}", typeof(T).Name, importPath);

            var fileProvider = new PhysicalFileProvider(importPath);

            var files = fileProvider.GetDirectoryContents("./").Where(IsImportFile).ToArray();
            var serializer = JsonSerializer.Create();

            var count = 0;
            foreach (var file in files)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Importing of {type}s stopped because of stopping application", typeof(T).Name);
                    break;
                }

                using (var streamReader = new StreamReader(file.CreateReadStream()))
                {
                    var jsonReader = new JsonTextReader(streamReader);
                    var existingOrders = serializer.Deserialize<T[]>(jsonReader);
                    count += existingOrders.Length;
                    UpdateItems(existingOrders);
                }
            }

            _storage.ResetModifiedDates();

            _logger.LogInformation("Imported up to {count} {type}s", count, typeof(T).Name);

            return Task.CompletedTask;
        }

        void UpdateItems(T[] existingOrders)
        {
            _storage.AddRange(existingOrders);
        }

        bool IsImportFile(IFileInfo fileInfo)
        {
            if (fileInfo.IsDirectory)
            {
                return false;
            }

            if (!fileInfo.Name.StartsWith(_startFile, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return (fileInfo.Name.EndsWith(".json", StringComparison.OrdinalIgnoreCase));
        }

        readonly IShopifyStorage<T> _storage;
        readonly ILogger _logger;
        readonly string _startFile;
    }
}
