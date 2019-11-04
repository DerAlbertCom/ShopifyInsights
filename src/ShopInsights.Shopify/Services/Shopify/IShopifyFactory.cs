namespace ShopInsights.Shopify.Services.Shopify
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
