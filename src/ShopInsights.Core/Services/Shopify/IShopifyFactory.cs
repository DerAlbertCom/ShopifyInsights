using ShopifySharp;

 namespace ShopInsights.Core.Services.Shopify
{
    public interface IShopifyFactory
    {
        MetaFieldService CreateMetaFieldService();
        IShopifyProductService CreateProductService();
        IShopifyOrderService CreateOrderService();
        IShopifyCustomerService CreateCustomerService();
    }
}
