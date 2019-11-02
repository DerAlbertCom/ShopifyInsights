using ShopifySharp;

namespace ShopInsights.Core.Services.Loaders
{
    public interface IProductLoader : ILoader<Product>
    {
    }

    public interface ICustomerLoader : ILoader<Customer>
    {

    }

    public interface IOrderLoader : ILoader<Order>
    {

    }
}
