using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopifySharp;
using ShopInsights.Core.Models;

namespace ShopInsights.Web.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : Controller
    {
        public ReportsController(IOrderStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("sales")]
        public ActionResult<SalesModel> Index(DateTime? from, DateTime? to, string location="all")
        {
            from = from ?? DateTime.Now;
            to = to ?? DateTime.Now;


            if (to < from)
            {
                return NotFound();
            }

            var list = new List<(DateTime date, decimal sum)>();
            var current = from.Value;

            Func<Order, bool> filter = NotCancelledOrder;
            if (string.IsNullOrWhiteSpace(location))
            {
                filter = OnlineOrder;
            }
            else if (long.TryParse(location, out var locationId))
            {
                filter = order => OrderOnLocation(order, locationId);
            }

            while (current <= to.Value)
            {
                var orders = _storage.GetForDate(current);
                var sum = orders.Where(filter).Sum(o => o.TotalPrice.Value);
                list.Add((current, sum));
                current = current.AddDays(1);
            }

            var model = new SalesModel
            {
                Labels = list.Select(o => o.date.ToShortDateString()).ToArray(),
                Data = list.Select(o => o.sum).ToArray()
            };
            return model;

            static bool NotCancelledOrder(Order order)
            {
                return !string.Equals(order.FinancialStatus, "voided", StringComparison.OrdinalIgnoreCase);
            }

            static bool OnlineOrder(Order order)
            {
                if (!order.LocationId.HasValue)
                {
                    return NotCancelledOrder(order);
                }

                return false;
            }

            static bool OrderOnLocation(Order order, long locationId)
            {
                if (order.LocationId.HasValue && order.LocationId.Value == locationId)
                {
                    return NotCancelledOrder(order);
                }

                return false;
            }
        }



        private readonly IOrderStorage _storage;
    }
}
