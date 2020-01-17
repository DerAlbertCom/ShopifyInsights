using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Services;

namespace ShopInsights.Shopify.Models
{
    public abstract class ShopifyStorage<T> : IShopifyStorage<T> where T : ShopifyObject
    {
        readonly Func<T, DateTimeOffset?> _updateSelector;
        readonly ISourceDataChangedService _changedService;
        readonly Func<T, DateTimeOffset?> _createSelector;

        protected ShopifyStorage(ISourceDataChangedService changedService, IOptions<ShopInstanceOptions> optionsAccessor, Func<T, DateTimeOffset?> createSelector,
            Func<T, DateTimeOffset?> updateSelector)
        {
            _timeZoneInfo = optionsAccessor.Value.TimeZoneInfo;
            _changedService = changedService;
            _createSelector = createSelector ?? throw new ArgumentNullException(nameof(createSelector));
            _updateSelector = updateSelector ?? throw new ArgumentNullException(nameof(updateSelector));
        }


        public T[] GetForDate(DateTime date)
        {
            date = _timeZoneInfo.GetTimeZoneCorrectedDate(date);

            if (_dictionary.TryGetValue(date, out var orders))
            {
                return orders.Values.OrderBy(o => o.Id.Value).ToArray();
            }

            return Array.Empty<T>();
        }

        public T GetById(long id)
        {
            return _allItemsDictionary[id];
        }

        public IEnumerable<DateTime> DatesWithModifiedItems
        {
            get { return _modifiedDates.OrderBy(d => d); }
        }

        public IEnumerable<T> All
        {
            get
            {
                foreach (var date in AllDates)
                {
                    var orders = GetDictionary(date).Values.OrderBy(o => o.Id);
                    foreach (var order in orders)
                    {
                        yield return order;
                    }
                }
            }
        }

        public IEnumerable<DateTime> AllDates => _dictionary.Keys.OrderBy(d => d);

        public void ResetModifiedDates()
        {
            _modifiedDates.Clear();
        }

        public async Task AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var createdNew = _createSelector(item);
                var updatedNew = _updateSelector(item);
                if (!createdNew.HasValue)
                {
                    continue;
                }

                var orderDictionary = GetDictionary(createdNew.Value);
                if (orderDictionary.TryGetValue(item.Id.Value, out var existingOrder))
                {
                    var updatedExisting = _updateSelector(existingOrder);

                    if (!updatedExisting.HasValue)
                    {
                        orderDictionary.Update(item);
                        UpdateModifiedItemDate(item);
                        await _changedService.Updated(item);
                    }
                    else if (updatedNew.Value > updatedExisting.Value)
                    {
                        orderDictionary.Update(item);
                        UpdateModifiedItemDate(item);
                        await _changedService.Updated(item);
                    }
                }
                else
                {
                    orderDictionary.Add(item);
                    UpdateModifiedItemDate(item);
                    await _changedService.Added(item);
                }
            }

        }

        void UpdateModifiedItemDate(T newOrder)
        {
            var created = _createSelector(newOrder);

            if (!created.HasValue) return;

            var date = _timeZoneInfo.GetTimeZoneCorrectedDate(created.Value);
            _modifiedDates.Add(date);
            _allItemsDictionary[newOrder.Id.Value] = newOrder;
        }

        ShopifyDictionary<long,T> GetDictionary(DateTimeOffset orderCreatedAt)
        {
            var date = _timeZoneInfo.GetTimeZoneCorrectedDate(orderCreatedAt);

            if (_dictionary.TryGetValue(date, out var orders))
            {
                return orders;
            }

            _dictionary[date] = new ShopifyDictionary<long, T>(o=>o.Id);
            return _dictionary[date];
        }

        readonly Dictionary<DateTime, ShopifyDictionary<long,T>>
            _dictionary = new Dictionary<DateTime, ShopifyDictionary<long,T>>();

        readonly Dictionary<long,T> _allItemsDictionary = new Dictionary<long, T>();
        readonly TimeZoneInfo _timeZoneInfo;
        readonly HashSet<DateTime> _modifiedDates = new HashSet<DateTime>();
    }
}
