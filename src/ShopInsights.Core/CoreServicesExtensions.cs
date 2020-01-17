using Microsoft.Extensions.DependencyInjection;
using ShopInsights.Services;

namespace ShopInsights
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services.AddTransient<ISourceDataChangedService, SourceDataChangedService>();
        }
    }
}
