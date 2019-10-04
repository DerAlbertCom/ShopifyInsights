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
            services.TryAddTransient<IOrderFilesImporter,OrderFilesImporter>();
            return services;
        }
    }
}
