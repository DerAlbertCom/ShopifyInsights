using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Services;
using ShopInsights.Shopify.Services.FetchAndStore;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Shopify
{
    public static class ShopifyServiceCollectionExtensions
    {
        public static IServiceCollection AddShopifyServices(this IServiceCollection services)
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

            services.TryAddTransient<IShopifyOrderFilesReader,ShopifyOrderFilesReader>();
            services.TryAddTransient<IShopifyProductFilesReader,ShopifyProductFilesReader>();
            services.TryAddTransient<IShopifyCustomerFilesReader,ShopifyCustomerFilesReader>();
            services.TryAddTransient<IShopifyMetaFieldFilesReader,ShopifyMetaFieldFilesReader>();
            services.TryAddTransient<IShopifyLocationFilesReader,ShopifyLocationFilesReader>();

            services.AddTransient<IShopifyOrderFilesWriter, ShopifyOrderFileWriter>();
            services.AddTransient<IShopifyProductFilesWriter, ShopifyProductFileWriter>();
            services.AddTransient<IShopifyCustomerFilesWriter, ShopifyCustomerFileWriter>();
            services.AddTransient<IShopifyMetaFieldFilesWriter, ShopifyMetaFieldFileWriter>();
            services.AddTransient<IShopifyLocationFilesWriter, ShopifyLocationFileWriter>();

            return services
                .AddTransient<IShopifyOrderFetcher, ShopifyOrderFetcher>()
                .AddTransient<IProductShopifyFetcher,ProductShopifyFetcher>()
                .AddTransient<IShopifyCustomerFetcher, ShopifyCustomerFetcher>()
                .AddTransient<IShopifyMetaFieldFetcher, ShopifyMetaFieldFetcher>()
                .AddTransient<IShopifyLocationFetcher, ShopifyLocationFetcher>()
                .AddTransient<IShopifyFactory, ShopifyFactory>()
                .AddTransient<IShopifyCustomerService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateCustomerService())
                .AddTransient<IShopifyOrderService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateOrderService())
                .AddTransient<IShopifyProductService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateProductService())
                .AddTransient<IShopifyLocationService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateLocationService())
                .AddTransient<IShopifyMetaFieldService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateMetaFieldService());
        }
    }
}
