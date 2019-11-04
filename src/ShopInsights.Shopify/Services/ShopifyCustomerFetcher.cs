using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    class ShopifyCustomerFetcher : IShopifyCustomerFetcher
    {
        private readonly IShopifyCustomerService _customerService;
        private readonly ILogger<ShopifyCustomerFetcher> _logger;

        public ShopifyCustomerFetcher(IShopifyCustomerService customerService, ILogger<ShopifyCustomerFetcher> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Customer>> GetUpdatedSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogDebug("Importing customers from Shopify since {dateTime}", sinceDate);

            var customers = new Dictionary<long,Customer>();

            IReadOnlyCollection<Customer> loadedCustomer;

            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Customer>();
                }

                loadedCustomer = await _customerService.ListUpdatedSinceAsync(sinceDate);
                _logger.LogInformation("Fetched {count} customers", loadedCustomer.Count);

                if (customers.AddUnique(loadedCustomer))
                {
                    var maxUpdates = loadedCustomer.Max(o => o.UpdatedAt);
                    _logger.LogDebug("Fetching rest of customers from Shopify since {dateTime}", maxUpdates);
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


            } while (loadedCustomer.Any());

            return customers.Values;

        }
    }
}
