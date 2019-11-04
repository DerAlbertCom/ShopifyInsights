using Microsoft.Extensions.Options;
using ShopifySharp;

namespace ShopInsights.Shopify.Services.Shopify
{
    public class ShopifyFactory : IShopifyFactory
    {
        private readonly IOptions<ShopifyOptions> _optionsAccessor;

        public ShopifyFactory(IOptions<ShopifyOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        public IShopifyMetaFieldService CreateMetaFieldService()
        {
            var options = _optionsAccessor.Value;
            return new ShopifyMetaFieldService(new MetaFieldService(options.ShopUrl, options.Password));
        }

        public IShopifyProductService CreateProductService()
        {
            var options = _optionsAccessor.Value;
            return new ShopifyProductService(new ProductService(options.ShopUrl, options.Password));
        }

        public IShopifyOrderService CreateOrderService()
        {
            var options = _optionsAccessor.Value;
            return new ShopifyOrderService(new OrderService(options.ShopUrl, options.Password));
        }

        public IShopifyCustomerService CreateCustomerService()
        {
            var options = _optionsAccessor.Value;
            return new ShopifyCustomerService(new CustomerService(options.ShopUrl, options.Password));
        }

        public IShopifyLocationService CreateLocationService()
        {
            var options = _optionsAccessor.Value;
            return new ShopifyLocationService(new LocationService(options.ShopUrl, options.Password));
        }
    }
}
