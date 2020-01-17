using System.Reflection;
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

        public static IServiceCollection AddDataChangedEvents(this IServiceCollection serviceCollection,
            params Assembly[] assemblies)
        {
            var scanner = new DataChangedEventScanner();
            scanner.ScanAssemblies(assemblies);
            var repository = new DataChangedEventRepository(scanner.Added, scanner.Updated, scanner.Removed);
            return serviceCollection.AddSingleton<IDataChangedEventRepository>(repository);
        }
    }
}
