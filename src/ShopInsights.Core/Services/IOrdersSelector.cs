using System;
using ShopifySharp;
using ShopInsights.Core.Models;

namespace ShopInsights.Core.Services
{
    internal interface IOrdersSelector
    {
        Order[] SelectForDate(OrderDictionary dictionary, DateTime dateTime);
    }
}
