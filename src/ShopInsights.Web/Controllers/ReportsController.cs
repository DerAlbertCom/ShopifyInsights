using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Web.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : Controller
    {
        public ReportsController(IShopifyOrderStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("daily")]
        public ActionResult<SalesModel> Index(DateTime? from, DateTime? to, string location="all")
        {
            from = from ?? DateTime.Now;
            to = to ?? DateTime.Now;


            if (to < from)
            {
                return NotFound();
            }

            var list = SumSalesPerDay(@from.Value, to.Value, location);

            var model = new SalesModel
            {
                Labels = list.Select(o => o.date.ToShortDateString()).ToArray(),
                Data = list.Select(o => o.sum).ToArray()
            };
            return model;
        }

        [HttpGet("weeks")]
        public ActionResult<SalesModel> Weeks(DateTime? from, DateTime? to, string location="all")
        {
            from = from ?? DateTime.Now;
            to = to ?? DateTime.Now;



            if (to < from)
            {
                return NotFound();
            }

            var list = SumSalesPerDay(@from.Value, to.Value, location);

            var calendar = CultureInfo.CurrentCulture.Calendar;

            var weekDictionary = new Dictionary<string, decimal>();

            foreach (var (date, sum) in list)
            {
                var week = calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                var key = $"{date.Year}_{week}";
                if (weekDictionary.TryGetValue(key, out var weekSum))
                {
                    weekSum += sum;
                    weekDictionary[key] = weekSum;
                }
                else
                {
                    weekDictionary[key] = sum;
                }
            }

            var newList = new List<(DateTime date, decimal sum)>();
            for (var current = from.Value; current <= to.Value; current = current.AddDays(1))
            {
                var startOfWeek = StartOfWeek(current, DayOfWeek.Monday);
                if (newList.Any(d => d.date == startOfWeek))
                {
                    continue;
                }
                var week = calendar.GetWeekOfYear(startOfWeek, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                var key = $"{startOfWeek.Year}_{week}";
                if (weekDictionary.TryGetValue(key, out var weekSum))
                {
                    newList.Add((startOfWeek, weekSum));
                }

            }

            var model = new SalesModel
            {
                Labels = newList.Select(o => o.date.ToShortDateString()).ToArray(),
                Data = newList.Select(o => o.sum).ToArray()
            };
            return model;
        }
        [HttpGet("months")]
        public ActionResult<SalesModel> Months(DateTime? from, DateTime? to, string location="all")
        {
            from = from ?? DateTime.Now;
            to = to ?? DateTime.Now;



            if (to < from)
            {
                return NotFound();
            }

            var list = SumSalesPerDay(@from.Value, to.Value, location);


            var monthDictionary = new Dictionary<string, decimal>();

            foreach (var (date, sum) in list)
            {
                var key = $"{date.Year}_{date.Month}";
                if (monthDictionary.TryGetValue(key, out var mmonthSum))
                {
                    mmonthSum += sum;
                    monthDictionary[key] = mmonthSum;
                }
                else
                {
                    monthDictionary[key] = sum;
                }
            }

            var newList = new List<(DateTime date, decimal sum)>();
            for (var current = from.Value; current <= to.Value; current = current.AddDays(1))
            {
                var monthDate = new DateTime(current.Year, current.Month, 1);

                if (newList.Any(d => d.date == monthDate))
                {
                    continue;
                }

                var key = $"{current.Year}_{current.Month}";

                if (monthDictionary.TryGetValue(key, out var monthSum))
                {
                    newList.Add((monthDate, monthSum));
                }

            }

            var model = new SalesModel
            {
                Labels = newList.Select(o => o.date.ToShortDateString()).ToArray(),
                Data = newList.Select(o => o.sum).ToArray()
            };
            return model;
        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        List<(DateTime date, decimal sum)> SumSalesPerDay(DateTime @from, DateTime to, string location)
                                   {
            var list = new List<(DateTime date, decimal sum)>();
            var current = @from;

            Func<Order, bool> filter = OrderFilters.NotCancelledOrder;
            if (string.IsNullOrWhiteSpace(location))
            {
                filter = OrderFilters.OnlineOrder;
            }
            else if (long.TryParse(location, out var locationId))
            {
                filter = order => OrderFilters.OrderOnLocation(order, locationId);
            }

            while (current <= to)
            {
                var orders = _storage.GetForDate(current);
                var sum = orders.Where(filter).Sum(o => o.TotalPrice.Value);
                list.Add((current, sum));
                current = current.AddDays(1);
            }

            return list;
        }

        readonly IShopifyOrderStorage _storage;
    }
}
