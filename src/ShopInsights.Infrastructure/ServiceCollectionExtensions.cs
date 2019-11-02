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
            services.TryAddTransient<IOrderFilesReader,OrderFilesReader>();
            services.TryAddTransient<IProductFilesReader,ProductFilesReader>();
            services.TryAddTransient<ICustomerFilesReader,CustomerFilesReader>();
            services.TryAddTransient<IMetaFieldFilesReader,MetaFieldFilesReader>();

            services.AddTransient<IOrderFilesWriter, OrderFileWriter>();
            services.AddTransient<IProductFilesWriter, ProductFileWriter>();
            services.AddTransient<ICustomerFilesWriter, CustomerFileWriter>();
            services.AddTransient<IMetaFieldFilesWriter, MetaFieldFileWriter>();

            return services;
        }
    }
}
