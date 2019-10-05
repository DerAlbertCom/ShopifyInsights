using System;
using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Models
{
    public interface IOrderStorage
    {
        Order[] GetOrdersForDate(DateTime date);
        IEnumerable<DateTime> ModifiedDates { get; }
        IEnumerable<Order> AllOrders { get; }
        IEnumerable<DateTime> AllDates { get; }
        void ResetModifiedDates();
        void AddOrders(IEnumerable<Order> newOrders);
    }
}
