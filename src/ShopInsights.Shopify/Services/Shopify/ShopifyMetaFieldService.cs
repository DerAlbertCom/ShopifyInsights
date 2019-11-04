using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Core.Services.Shopify
{
    internal class ShopifyMetaFieldService : IShopifyMetaFieldService
    {
        private readonly MetaFieldService _metaFieldService;

        public ShopifyMetaFieldService(MetaFieldService metaFieldService)
        {
            _metaFieldService = metaFieldService;
        }

        public async Task<IReadOnlyCollection<MetaField>> ListUpdatedSinceAsync(DateTimeOffset sinceDate)
        {
            var filter = new MetaFieldFilter()
            {
                Order = "updated_at asc",
                Limit = 200,
                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            var metaFields = await _metaFieldService.ListAsync(filter);
            return metaFields.ToArray();
        }
    }
    internal class ShopifyLocationService : IShopifyLocationService
    {
        private readonly LocationService _locationService;

        public ShopifyLocationService(LocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IReadOnlyCollection<Location>> ListUpdatedSinceAsync()
        {
            var locations = await _locationService.ListAsync();
            return locations.ToArray();
        }
    }
}
