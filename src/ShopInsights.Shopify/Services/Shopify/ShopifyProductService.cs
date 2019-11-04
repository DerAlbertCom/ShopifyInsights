using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Core.Services.Shopify
{
    internal class ShopifyProductService : IShopifyProductService
    {
        private readonly ProductService _productService;

        public ShopifyProductService(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IReadOnlyCollection<Product>> ListUpdatedSinceAsync(DateTimeOffset sinceDate)
        {
            var filter = new ProductFilter()
            {
                Order = "updated_at asc",
                Limit = 200,
                PublishedStatus = "any",

                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            var products = await _productService.ListAsync(filter);
            return products.ToArray();
        }
    }
}
