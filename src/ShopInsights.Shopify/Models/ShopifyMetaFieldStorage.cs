﻿using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Services;

namespace ShopInsights.Shopify.Models
{
    public class ShopifyMetaFieldStorage : ShopifyStorage<MetaField>, IShopifyMetaFieldStorage
    {
        public ShopifyMetaFieldStorage(IOptions<ShopInstanceOptions> optionsAccessor,
            ISourceDataChangedService changedService) : base(changedService, optionsAccessor, c => c.CreatedAt,
            c => c.UpdatedAt)
        {
        }
    }
}
