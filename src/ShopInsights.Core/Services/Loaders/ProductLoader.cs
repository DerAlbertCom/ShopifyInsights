﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;

namespace ShopInsights.Core.Services.Loaders
{
    public class ProductLoader : Loader<Product>, IProductLoader
    {
        public ProductLoader(IOptions<StoreOptions> optionsAccessor, IProductShopifyFetcher shopifyFetcher,
            IProductStorage storage, IProductFilesStorage filesStorage, ILogger<ProductLoader> logger) : base(optionsAccessor, shopifyFetcher, storage,
            filesStorage, "products", product => product.UpdatedAt, logger)
        {
        }
    }
}
