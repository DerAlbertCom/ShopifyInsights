using ShopifySharp;

 namespace ShopInsights.Core.Services.Shopify
{
    public interface IShopifyFactory
    {
        IShopifyMetaFieldService CreateMetaFieldService();
        IShopifyProductService CreateProductService();
        IShopifyOrderService CreateOrderService();
        IShopifyCustomerService CreateCustomerService();
    }
}
