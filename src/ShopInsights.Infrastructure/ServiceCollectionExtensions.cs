using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopInsights.Core.Stores;
using ShopInsights.Infrastructure.Stores;

namespace ShopInsights.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.TryAddTransient<IShopifyOrderFilesReader,ShopifyOrderFilesReader>();
            services.TryAddTransient<IShopifyProductFilesReader,ShopifyProductFilesReader>();
            services.TryAddTransient<IShopifyCustomerFilesReader,ShopifyCustomerFilesReader>();
            services.TryAddTransient<IShopifyMetaFieldFilesReader,ShopifyMetaFieldFilesReader>();
            services.TryAddTransient<ILocationShopifyFilesReader,LocationShopifyFilesReader>();

            services.AddTransient<IShopifyOrderFilesWriter, ShopifyOrderFileWriter>();
            services.AddTransient<IShopifyProductFilesWriter, ShopifyProductFileWriter>();
            services.AddTransient<ICustomerShopifyFilesWriter, CustomerShopifyFileWriter>();
            services.AddTransient<IShopifyMetaFieldFilesWriter, ShopifyMetaFieldFileWriter>();
            services.AddTransient<ILocationShopifyFilesWriter, LocationShopifyFileWriter>();

            return services;
        }
    }
}
