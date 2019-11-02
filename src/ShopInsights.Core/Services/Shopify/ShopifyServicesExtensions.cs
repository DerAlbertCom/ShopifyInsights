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
                .AddTransient<IShopifyFactory, ShopifyFactory>()
                .AddTransient<IShopifyCustomerService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateCustomerService())
                .AddTransient<IShopifyOrderService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateOrderService())
                .AddTransient<IShopifyProductService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateProductService());
        }
    }
}
