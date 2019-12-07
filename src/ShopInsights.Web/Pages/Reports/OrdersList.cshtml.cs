using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Configuration;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Web.Pages.Reports
{
    public class OrdersList : PageModel
    {
        private readonly IShopifyOrderStorage _orderStorage;
        private TimeZoneInfo _timeZone;

        public OrdersList(IOptions<ShopInstanceOptions> optionsAccessor, IShopifyOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
            _timeZone = optionsAccessor.Value.TimeZoneInfo;
        }

        public void OnGet(DateTime orderDate)
        {
            Orders = _orderStorage.GetForDate(orderDate.Date)
                .Where(o => o.LocationId.HasValue)
                .Where(o => !string.Equals(o.FinancialStatus, "refunded", StringComparison.OrdinalIgnoreCase))
                .OrderBy(o => o.OrderNumber.Value).ToArray();

        }

        public IEnumerable<Order> Orders { get; set; }
    }
}
