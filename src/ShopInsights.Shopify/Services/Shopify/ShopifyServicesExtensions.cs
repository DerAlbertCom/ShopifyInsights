using Microsoft.Extensions.DependencyInjection;

namespace ShopInsights.Core.Services.Shopify
{
    public static class ShopifyServicesExtensions
    {
        public static IServiceCollection AddShopifyServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IOrderShopifyFetcher, OrderShopifyFetcher>()
                .AddTransient<IProductShopifyFetcher,ProductShopifyFetcher>()
                .AddTransient<ICustomerShopifyFetcher, CustomerShopifyFetcher>()
                .AddTransient<IMetaFieldShopifyFetcher, MetaFieldShopifyFetcher>()
                .AddTransient<ILocationShopifyFetcher, LocationShopifyFetcher>()
                .AddTransient<IShopifyFactory, ShopifyFactory>()
                .AddTransient<IShopifyCustomerService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateCustomerService())
                .AddTransient<IShopifyOrderService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateOrderService())
                .AddTransient<IShopifyProductService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateProductService())
                .AddTransient<IShopifyLocationService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateLocationService())
                .AddTransient<IShopifyMetaFieldService>(provider => provider.GetRequiredService<IShopifyFactory>().CreateMetaFieldService());
        }
    }
}
