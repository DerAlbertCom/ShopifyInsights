using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Core.Services.Shopify
{
    class ProductImporter : IProductImporter
    {
        private readonly IShopifyFactory _shopifyFactory;
        private readonly ILogger<ProductImporter> _logger;

        public ProductImporter(IShopifyFactory shopifyFactory, ILogger<ProductImporter> logger)
        {
            _shopifyFactory = shopifyFactory;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Product>> GetSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogInformation("Importing Product from Shopify since {dateTime}", sinceDate);
            var productService = _shopifyFactory.CreateProductService();

            var products = new Dictionary<long,Product>();

            IReadOnlyCollection<Product> loadedProducts;

            var filter = new ProductFilter()
            {
                Order = "updated_at asc",
                Limit = 200,
                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Product>();
                }

                loadedProducts = (await productService.ListAsync(filter)).ToArray();
                _logger.LogInformation("Fetched {count} products", loadedProducts.Count);
                if (products.AddUnique(loadedProducts))
                {
                    var maxUpdates = loadedProducts.Max(o => o.UpdatedAt);
                    _logger.LogInformation("Fetching rest of Product from Shopify since {dateTime}", maxUpdates);

                    if (maxUpdates.HasValue)
                    {
                        filter.UpdatedAtMin = maxUpdates.Value.Subtract(TimeSpan.FromSeconds(1));
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


            } while (loadedProducts.Any());

            return products.Values;
        }


    }
}
