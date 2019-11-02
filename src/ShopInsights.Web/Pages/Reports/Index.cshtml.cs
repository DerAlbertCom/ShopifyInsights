using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopInsights.Core.Models;

namespace ShopInsights.Web.Pages.Reports
{
    public class Index : PageModel
    {
        public Index(IOrderStorage storage)
        {
            _storage = storage;
        }

        public void OnGet(DateTime from, DateTime to)
        {
            From = from == DateTime.MinValue ? DateTime.Today.Subtract(TimeSpan.FromDays(31)) : from;
            To = to == DateTime.MinValue ? DateTime.Today : to;
            if (To < from) To = from.AddMonths(1);
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        private readonly IOrderStorage _storage;
    }
}
