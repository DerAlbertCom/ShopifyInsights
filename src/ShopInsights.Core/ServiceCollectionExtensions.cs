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
            services.TryAddSingleton<IShopifyOrderStorage, ShopifyOrderStorage>();
            services.TryAddSingleton<IShopifyProductStorage, ShopifyProductStorage>();
            services.TryAddSingleton<IShopifyCustomerStorage, ShopifyCustomerStorage>();
            services.TryAddSingleton<IShopifyMetaFieldStorage, ShopifyMetaFieldStorage>();
            services.TryAddSingleton<ILocationStorage, LocationStorage>();

            services.AddTransient<IShopifyProductShopifyFetchAndStoreService, ShopifyProductShopifyFetchAndStoreService>();
            services.AddTransient<IShopifyCustomerShopifyFetchAndStoreService, ShopifyCustomerShopifyFetchAndStoreService>();
            services.AddTransient<IShopifyOrderShopifyFetchAndStoreService, ShopifyOrderShopifyFetchAndStoreService>();
            services.AddTransient<IShopifyMetaFieldShopifyFetchAndStoreService, ShopifyMetaFieldShopifyFetchAndStoreService>();
            services.AddTransient<ILocationShopifyFetchAndStoreService, LocationShopifyFetchAndStoreService>();
            services.AddShopifyServices();
            return services;
        }
    }
}
