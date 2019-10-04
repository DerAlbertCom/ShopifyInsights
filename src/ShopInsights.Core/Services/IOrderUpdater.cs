using System;
using System.Collections.Generic;
using ShopifySharp;
using ShopInsights.Core.Models;

namespace ShopInsights.Core.Services
{
    public interface IOrderUpdater
    {
        void AddOrUpdate(OrderDictionary orders, Order order);
        ISet<DateTime> ModifiedDates { get; }
    }
}