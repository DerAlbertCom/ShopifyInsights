using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify.Services.FetchAndStore
{
    public abstract class ShopifyFetchAndStoreService<T> : IShopifyFetchAndStoreService<T> where T : ShopifyObject
    {
        private readonly IOptions<StoreOptions> _optionsAccessor;
        private readonly IShopifyFetcher<T> _fetcher;
        private readonly IShopifyStorage<T> _storage;
        private readonly IShopifyFilesWriter<T> _filesWriter;
        private readonly string _folder;
        private readonly Func<T, DateTimeOffset?> _updateSelector;
        private readonly ILogger _logger;

        protected ShopifyFetchAndStoreService(
            IOptions<StoreOptions> optionsAccessor,
            IShopifyFetcher<T> fetcher,
            IShopifyStorage<T> storage,
            IShopifyFilesWriter<T> filesWriter,
            string folder,
            Func<T, DateTimeOffset?> updateSelector,
            ILogger logger)
        {
            _optionsAccessor = optionsAccessor;
            _fetcher = fetcher;
            _storage = storage;
            _filesWriter = filesWriter;
            _folder = folder;
            _updateSelector = updateSelector;
            _logger = logger;
        }

        public async Task FetchUpdatesAndStoreAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Current {count} existing {type}s", _storage.All.Count(), typeof(T).Name);
            var maxUpdate = _storage.All.Max(_updateSelector);
            if (!maxUpdate.HasValue)
            {
                maxUpdate = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
            }

            _logger.LogInformation("Loading new {type}s", typeof(T).Name);
            var products = await _fetcher.GetUpdatedSinceAsync(maxUpdate.Value, stoppingToken);

            if (stoppingToken.IsCancellationRequested)
            {
                return;
            }

            _storage.AddRange(products);

            _logger.LogInformation("Storing newly fetched {type}s", typeof(T).Name);

            await _filesWriter.StoreFilesAsync(Path.Combine(_optionsAccessor.Value.GetFilePath(), _folder),
                stoppingToken);
        }
    }
}
