using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    internal class ShopifyMetaFieldFetcher : IShopifyMetaFieldFetcher
    {
        readonly IShopifyMetaFieldService _metaFieldService;
        readonly ILogger<ShopifyMetaFieldFetcher> _logger;

        public ShopifyMetaFieldFetcher(IShopifyMetaFieldService metaFieldService, ILogger<ShopifyMetaFieldFetcher> logger)
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
}
