using System;
using System.Collections.Generic;
using ShopifySharp;

namespace ShopInsights.Core.Models
{
    public interface IShopifyStorage<T>  where T : ShopifyObject
    {
        T[] GetForDate(DateTime date);

        T GetById(long id);

        IEnumerable<DateTime> DatesWithModifiedItems { get; }
        IEnumerable<T> All { get; }
        IEnumerable<DateTime> AllDates { get; }
        void ResetModifiedDates();
        void AddRange(IEnumerable<T> newOrders);
    }
}
