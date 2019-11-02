using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopInsights.Core.Services;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;
using ShopInsights.Infrastructure.Services;
using ShopInsights.Infrastructure.Stores;

namespace ShopInsights.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.TryAddTransient<IOrderFilesImporter,OrderFilesImporter>();
            services.TryAddTransient<IProductFilesImporter,ProductFilesImporter>();
            services.TryAddTransient<ICustomerFilesImporter,CustomerFilesImporter>();

            services.AddTransient<IOrderFilesStorage, OrderFileStorage>();
            services.AddTransient<IProductFilesStorage, ProductFileStorage>();
            services.AddTransient<ICustomerFilesStorage, CustomerFileStorage>();

            return services;
        }
    }
}
