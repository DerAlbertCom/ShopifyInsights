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
    class CustomerImporter : ICustomerImporter
    {
        private readonly IShopifyFactory _shopifyFactory;
        private readonly ILogger<CustomerImporter> _logger;

        public CustomerImporter(IShopifyFactory shopifyFactory, ILogger<CustomerImporter> logger)
        {
            _shopifyFactory = shopifyFactory;
            _logger = logger;
        }

        public async Task<IReadOnlyCollection<Customer>> GetSinceAsync(DateTimeOffset sinceDate,
            CancellationToken stoppingToken)
        {
            _logger.LogDebug("Importing customers from Shopify since {dateTime}", sinceDate);

            var customerService = _shopifyFactory.CreateCustomerService();

            var customers = new Dictionary<long,Customer>();

            IReadOnlyCollection<Customer> loadedCustomer;

            var filter = new ListFilter()
            {
                Order = "updated_at asc",
                Limit = 200,
                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            do
            {
                if (stoppingToken.IsCancellationRequested)
                {
                    return Array.Empty<Customer>();
                }
                loadedCustomer = (await customerService.ListAsync(filter)).ToArray();
                _logger.LogInformation("Fetched {count} customers", loadedCustomer.Count);

                if (customers.AddUnique(loadedCustomer))
                {
                    var maxUpdates = loadedCustomer.Max(o => o.UpdatedAt);
                    _logger.LogDebug("Fetching rest of customers from Shopify since {dateTime}", maxUpdates);
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


            } while (loadedCustomer.Any());

            return customers.Values;

        }
    }
}
