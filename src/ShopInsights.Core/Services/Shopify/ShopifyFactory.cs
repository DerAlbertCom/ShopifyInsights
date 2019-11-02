using Microsoft.Extensions.Options;
using ShopifySharp;

namespace ShopInsights.Core.Services.Shopify
{
    public class ShopifyFactory : IShopifyFactory
    {
        private readonly IOptions<ShopifyAuthenticationOptions> _optionsAccessor;

        public ShopifyFactory(IOptions<ShopifyAuthenticationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        public MetaFieldService CreateMetaFieldService()
        {
            var options = _optionsAccessor.Value;
            return new MetaFieldService(options.ShopUrl, options.Password);
        }

        public ProductService CreateProductService()
        {
            var options = _optionsAccessor.Value;
            return new ProductService(options.ShopUrl, options.Password);
        }

        public OrderService CreateOrderService()
        {
            var options = _optionsAccessor.Value;
            return new OrderService(options.ShopUrl, options.Password);
        }

        public CustomerService CreateCustomerService()
        {
            var options = _optionsAccessor.Value;
            return new CustomerService(options.ShopUrl, options.Password);
        }
    }
}
