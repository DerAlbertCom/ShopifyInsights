using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;

namespace ShopInsights.Core.Services.Shopify
{
    class MetaFieldShopifyFetcher : IMetaFieldShopifyFetcher
    {
        private readonly IShopifyMetaFieldService _metaFieldService;
        private readonly ILogger<MetaFieldShopifyFetcher> _logger;

        public MetaFieldShopifyFetcher(IShopifyMetaFieldService metaFieldService, ILogger<MetaFieldShopifyFetcher> logger)
        {
            _metaFieldService = metaFieldService;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<MetaField>> GetUpdatedSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogDebug("Importing MetaFields from Shopify since {dateTime}", sinceDate);

            var metaFields = new Dictionary<long,MetaField>();

            IReadOnlyCollection<MetaField> loadedMetaField;

            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<MetaField>();
                }

                loadedMetaField = await _metaFieldService.ListUpdatedSinceAsync(sinceDate);
                _logger.LogInformation("Fetched {count} MetaFields", loadedMetaField.Count);

                if (metaFields.AddUnique(loadedMetaField))
                {
                    var maxUpdates = loadedMetaField.Max(o => o.UpdatedAt);
                    _logger.LogDebug("Fetching rest of MetaFields from Shopify since {dateTime}", maxUpdates);
                    if (maxUpdates.HasValue)
                    {
                        sinceDate = maxUpdates.Value;
                    }
                    else
                    {
                        break;
                    }

                }
                else
                {
                    break;
                }


            } while (loadedMetaField.Any());

            return metaFields.Values;

        }
    }
    class LocationShopifyFetcher : ILocationShopifyFetcher
    {
        private readonly IShopifyLocationService _locationService;
        private readonly ILogger<LocationShopifyFetcher> _logger;

        public LocationShopifyFetcher(IShopifyLocationService locationService, ILogger<LocationShopifyFetcher> logger)
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
