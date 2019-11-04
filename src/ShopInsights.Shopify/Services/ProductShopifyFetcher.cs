using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    class ProductShopifyFetcher : IProductShopifyFetcher
    {
        private readonly IShopifyFactory _shopifyFactory;
        private readonly ILogger<ProductShopifyFetcher> _logger;

        public ProductShopifyFetcher(IShopifyFactory shopifyFactory, ILogger<ProductShopifyFetcher> logger)
        {
            _shopifyFactory = shopifyFactory;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Product>> GetUpdatedSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogInformation("Importing Product from Shopify since {dateTime}", sinceDate);
            var productService = _shopifyFactory.CreateProductService();

            var products = new Dictionary<long,Product>();

            IReadOnlyCollection<Product> loadedProducts;

            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Product>();
                }

                loadedProducts = await productService.ListUpdatedSinceAsync(sinceDate);

                _logger.LogInformation("Fetched {count} products", loadedProducts.Count);
                if (products.AddUnique(loadedProducts))
                {
                    var maxUpdates = loadedProducts.Max(o => o.UpdatedAt);
                    _logger.LogInformation("Fetching rest of Product from Shopify since {dateTime}", maxUpdates);

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


            } while (loadedProducts.Any());

            return products.Values;
        }


    }
}
