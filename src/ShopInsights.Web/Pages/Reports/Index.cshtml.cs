using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ShopInsights.Core.Models;

namespace ShopInsights.Web.Pages.Reports
{
    public class Index : PageModel
    {
        public Index(IShopifyOrderStorage shopifyOrderStorage, ILocationStorage locationStorage)
        {
            _shopifyOrderStorage = shopifyOrderStorage;
            _locationStorage = locationStorage;
        }

        public void OnGet(DateTime from, DateTime to)
        {
            From = from == DateTime.MinValue ? DateTime.Today.Subtract(TimeSpan.FromDays(31)) : from;
            To = to == DateTime.MinValue ? DateTime.Today : to;
            if (To < from) To = from.AddMonths(1);

            LocationItems.Add(new SelectListItem("Alle","all"));
            LocationItems.Add(new SelectListItem("Online",""));
            foreach (var location in _locationStorage.All)
            {
                LocationItems.Add(new SelectListItem(location.Name,location.Id.Value.ToString()));

            }
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public List<SelectListItem> LocationItems { get; } = new List<SelectListItem>();
        private readonly IShopifyOrderStorage _shopifyOrderStorage;
        private readonly ILocationStorage _locationStorage;
    }
}
