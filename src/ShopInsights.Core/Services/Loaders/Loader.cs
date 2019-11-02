using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.Loaders
{
    public abstract class Loader<T> : ILoader<T> where T : ShopifyObject
    {
        private readonly IOptions<StoreOptions> _optionsAccessor;
        private readonly IShopifyFetcher<T> _shopifyFetcher;
        private readonly IShopifyStorage<T> _storage;
        private readonly IFilesStorage<T> _filesStorage;
        private readonly string _folder;
        private readonly Func<T, DateTimeOffset?> _updateSelector;
        private readonly ILogger _logger;

        protected Loader(IOptions<StoreOptions> optionsAccessor, IShopifyFetcher<T> shopifyFetcher,
            IShopifyStorage<T> storage, IFilesStorage<T> filesStorage, string folder,
            Func<T, DateTimeOffset?> updateSelector, ILogger logger)
        {
            _optionsAccessor = optionsAccessor;
            _shopifyFetcher = shopifyFetcher;
            _storage = storage;
            _filesStorage = filesStorage;
            _folder = folder;
            _updateSelector = updateSelector;
            _logger = logger;
        }

        public async Task LoadNewAndSaveChangesAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Current {count} existing {type}s", _storage.All.Count(), typeof(T).Name);
            var maxUpdate = _storage.All.Max(_updateSelector);
            if (!maxUpdate.HasValue)
            {
                maxUpdate = new DateTimeOffset(2000,1,1,0,0,0,TimeSpan.Zero);
            }

            _logger.LogInformation("Loading new {type}s", typeof(T).Name);
            var products = await _shopifyFetcher.GetSinceAsync(maxUpdate.Value, stoppingToken);

            if (stoppingToken.IsCancellationRequested)
            {
                return;
            }

            _storage.AddRange(products);

            _logger.LogInformation("Storing newly fetched {type}s", typeof(T).Name);

            await _filesStorage.StoreFilesAsync(Path.Combine(_optionsAccessor.Value.FilePath, _folder),
                stoppingToken);
        }
    }
}
