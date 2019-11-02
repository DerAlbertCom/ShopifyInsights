using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace ShopInsights.Core.Services.Shopify
{
    public static class ShopifyServicesExtensions
    {
        public static IServiceCollection AddShopifyServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IOrderImporter, OrderImporter>()
                .AddTransient<IProductImporter,ProductImporter>()
                .AddTransient<ICustomerImporter, CustomerImporter>()
                .AddTransient<IShopifyFactory, ShopifyFactory>();
        }
    }
}
