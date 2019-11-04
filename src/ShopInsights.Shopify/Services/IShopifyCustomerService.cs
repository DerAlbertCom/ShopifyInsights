using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    public interface IShopifyCustomerService
    {
        Task<IReadOnlyCollection<Customer>> ListUpdatedSinceAsync(DateTimeOffset sinceDate);
    }
}
