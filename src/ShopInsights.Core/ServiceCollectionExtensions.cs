using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services.FetchAndStore;
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
            services.TryAddSingleton<IMetaFieldStorage, MetaFieldStorage>();

            services.AddTransient<IProductFetchAndStoreService, ProductFetchAndStoreService>();
            services.AddTransient<ICustomerFetchAndStoreService, CustomerFetchAndStoreService>();
            services.AddTransient<IOrderFetchAndStoreService, OrderFetchAndStoreService>();
            services.AddTransient<IMetaFieldFetchAndStoreService, MetaFieldFetchAndStoreService>();
            services.AddShopifyServices();
            return services;
        }
    }
}
