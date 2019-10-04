using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopInsights.Core.Models;
using ShopInsights.Core.Services;

namespace ShopInsights.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.TryAddTransient<IOrdersSelector,OrdersSelector>();
            services.TryAddTransient<IOrderUpdater, OrderUpdater>();
            services.TryAddSingleton<IOrderStorage, OrderStorage>();
            return services;
        }
    }
}
