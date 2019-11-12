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
    public class Orders : PageModel
    {
        private readonly IShopifyOrderStorage _orderStorage;
        private readonly ILocationStorage _locationStorage;
        private TimeZoneInfo _timeZone;

        public Orders(IShopifyOrderStorage orderStorage, ILocationStorage locationStorage, IOptions<ShopInstanceOptions> optionsAccessor)
        {
            _orderStorage = orderStorage;
            _locationStorage = locationStorage;
            _timeZone = optionsAccessor.Value.TimeZoneInfo;
        }

        public void OnGet()
        {
            var locationDates = _orderStorage.All.Where(o => o.LocationId.HasValue)
                .Select(o => new {OrderDate = _timeZone.GetTimeZoneCorrectedDate(o.CreatedAt.Value), o.LocationId})
                .ToArray();

           Locations = new Dictionary<string, DateTime[]>();
           foreach (var location in _locationStorage.All)
           {
               var dates = locationDates.Where(ld => ld.LocationId == location.Id)
                   .Select(o => o.OrderDate).Distinct().OrderBy(d => d).ToArray();
               Locations[location.Name] = dates;
           }
        }

        public Dictionary<string, DateTime[]> Locations { get; set; }
    }
}
