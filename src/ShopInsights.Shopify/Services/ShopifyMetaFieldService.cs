using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Shopify.Services
{
    internal class ShopifyMetaFieldService : IShopifyMetaFieldService
    {
        readonly MetaFieldService _metaFieldService;

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
}
