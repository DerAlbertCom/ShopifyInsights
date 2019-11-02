using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopInsights.Core.Services.Shopify
{
    internal class ShopifyCustomerService : IShopifyCustomerService
    {
        private readonly CustomerService _customerService;

        public ShopifyCustomerService(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IReadOnlyCollection<Customer>> ListUpdatedSinceAsync(DateTimeOffset sinceDate)
        {
            var filter = new ListFilter()
            {
                Order = "updated_at asc",
                Limit = 200,
                UpdatedAtMin =  sinceDate.Subtract(TimeSpan.FromSeconds(1))
            };
            var customers = await _customerService.ListAsync(filter);
            return customers.ToArray();
        }
    }
}
