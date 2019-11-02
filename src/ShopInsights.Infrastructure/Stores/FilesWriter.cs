using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public abstract class FilesWriter<T> : IFilesWriter<T> where T : ShopifyObject
    {
        private readonly IShopifyStorage<T> _storage;
        private readonly string _startFile;
        private readonly ILogger _logger;

        protected FilesWriter(IShopifyStorage<T> storage, string startFile, ILogger logger)
        {
            _storage = storage;
            _startFile = startFile;
            _logger = logger;
        }

        public Task StoreFilesAsync(string storePath, CancellationToken stoppingToken)
        {
            if (!Directory.Exists(storePath))
            {
                _logger.LogDebug("Creating folder {folder}", storePath);
                Directory.CreateDirectory(storePath);
            }

            foreach (var dateTime in _storage.DatesWithModifiedItems)
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                var items = _storage.GetForDate(dateTime);

                _logger.LogInformation("Storing {count} {type} for {date}", items?.Length, typeof(T).Name, dateTime);

                if (items == null || items.Length == 0)
                {
                    continue;
                }

                var fileName = $"{_startFile}-{dateTime.Year}-{dateTime.Month}-{dateTime.Day}.json";

                var fullPath = Path.Combine(storePath, fileName);

                var serializer = JsonSerializer.Create();

                using (var fileStream = File.CreateText(fullPath))
                {
                    serializer.Serialize(fileStream, items);
                }
            }

            return Task.CompletedTask;
        }
    }
}
