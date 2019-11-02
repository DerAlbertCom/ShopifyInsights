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

            Func<Order, bool> filter = AllOrders;
            if (string.IsNullOrWhiteSpace(location))
            {
                filter = OnlineOrders;
            }
            else if (long.TryParse(location, out var locationId))
            {
                filter = order => LocationOrders(order, locationId);
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

            static bool AllOrders(Order order)
            {
                return true;
            }

            static bool OnlineOrders(Order order)
            {
                return !order.LocationId.HasValue;
            }

            static bool LocationOrders(Order order, long locationId)
            {
                return order.LocationId.HasValue && order.LocationId.Value == locationId;
            }
        }



        private readonly IOrderStorage _storage;
    }
}
