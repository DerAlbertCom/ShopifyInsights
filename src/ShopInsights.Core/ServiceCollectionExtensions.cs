using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.Loaders;
using ShopInsights.Core.Services.Shopify;

namespace ShopInsights.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IOrderStorage, OrderStorage>();
            services.TryAddSingleton<IProductStorage, ProductStorage>();
            services.TryAddSingleton<ICustomerStorage, CustomerStorage>();

            services.AddTransient<IProductLoader, ProductLoader>();
            services.AddTransient<ICustomerLoader, CustomerLoader>();
            services.AddTransient<IOrderLoader, OrderLoader>();
            services.AddShopifyServices();
            return services;
        }
    }
}
