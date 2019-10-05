using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<SalesModel> Index(DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.Now;
            to = to ?? DateTime.Now;

            if (to < from)
            {
                return NotFound();
            }

            var list = new List<(DateTime date, decimal sum)>();
            var current = from.Value;
            while (current <= to.Value)
            {
                var orders = _storage.GetOrdersForDate(current);
                var sum = orders.Sum(o => o.TotalPrice.Value);
                list.Add((current, sum));
                current = current.AddDays(1);
            }

            var model = new SalesModel
            {
                Labels = list.Select(o => o.date.ToShortDateString()).ToArray(),
                Data = list.Select(o => o.sum).ToArray()
            };
            return model;
        }



        private readonly IOrderStorage _storage;
    }

    public class SalesModel
    {
        public decimal[] Data { get; set; }
        public string[] Labels { get; set; }
    }
}
