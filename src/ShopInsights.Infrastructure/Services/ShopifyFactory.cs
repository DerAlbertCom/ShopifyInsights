using Microsoft.Extensions.Options;
using ShopifySharp;

namespace ShopInsights.Infrastructure.Services
{
    public class ShopifyFactory : IShopifyFactory
    {
        readonly IOptionsSnapshot<ShopifyAuthenticationOptions> _optionsAccessor;
        ShopifyAuthenticationOptions _options;

        public ShopifyFactory(IOptionsSnapshot<ShopifyAuthenticationOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        public MetaFieldService CreateMetaFieldService()
        {
            _options = _optionsAccessor.Value;
            return new MetaFieldService(_options.ShopUrl, _options.Password);
        }

        public ProductService CreateProductService()
        {
            _options = _optionsAccessor.Value;
            return new ProductService(_options.ShopUrl, _options.Password);
        }

        public OrderService CreateOrderService()
        {
            _options = _optionsAccessor.Value;
            return new OrderService(_options.ShopUrl, _options.Password);
        }
    }
}
