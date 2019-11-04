using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    class ShopifyLocationFetcher : IShopifyLocationFetcher
    {
        private readonly IShopifyLocationService _locationService;
        private readonly ILogger<ShopifyLocationFetcher> _logger;

        public ShopifyLocationFetcher(IShopifyLocationService locationService, ILogger<ShopifyLocationFetcher> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Location>> GetUpdatedSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogDebug("Importing Locations from Shopify since {dateTime}", sinceDate);

            var locations = new Dictionary<long,Location>();

            IReadOnlyCollection<Location> loadedLocation;

            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Location>();
                }

                loadedLocation = await _locationService.ListUpdatedSinceAsync();
                _logger.LogInformation("Fetched {count} Locations", loadedLocation.Count);

                if (!locations.AddUnique(loadedLocation))
                {
                    break;
                }


            } while (loadedLocation.Any());

            return locations.Values;

        }
    }
}