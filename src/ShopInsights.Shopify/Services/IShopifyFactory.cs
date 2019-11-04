namespace ShopInsights.Shopify.Services
{
    public interface IShopifyFactory
    {
        IShopifyMetaFieldService CreateMetaFieldService();
        IShopifyProductService CreateProductService();
        IShopifyOrderService CreateOrderService();
        IShopifyCustomerService CreateCustomerService();
        IShopifyLocationService CreateLocationService();
    }
}
