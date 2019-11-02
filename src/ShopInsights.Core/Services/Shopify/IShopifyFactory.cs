﻿using ShopifySharp;

 namespace ShopInsights.Core.Services.Shopify
{
    public interface IShopifyFactory
    {
        MetaFieldService CreateMetaFieldService();
        ProductService CreateProductService();
        OrderService CreateOrderService();
        CustomerService CreateCustomerService();
    }
}
