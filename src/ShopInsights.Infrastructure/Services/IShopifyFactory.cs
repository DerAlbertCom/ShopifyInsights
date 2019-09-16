﻿using ShopifySharp;

 namespace ShopInsights.Infrastructure.Services
{
    public interface IShopifyFactory
    {
        MetaFieldService CreateMetaFieldService();
        ProductService CreateProductService();
        OrderService CreateOrderService();
    }
}