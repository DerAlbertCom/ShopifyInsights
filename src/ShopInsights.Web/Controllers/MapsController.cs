using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShopifySharp;
using ShopInsights.Shopify.Models;

namespace ShopInsights.Web.Controllers
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : Controller
    {
        private readonly IShopifyOrderStorage _shopifyOrderStorage;

        public MapsController(IShopifyOrderStorage shopifyOrderStorage)
        {
            _shopifyOrderStorage = shopifyOrderStorage;
        }
        [HttpGet("sales")]
        public IEnumerable<Feature> Index()
        {
            var allOrders = _shopifyOrderStorage.All;
            var ordersWithCoordinates = allOrders.Where(HasCoordinates);
            foreach (var order in ordersWithCoordinates)
            {
                var feature = new Feature()
                {
                    Id = order.Id.ToString(),
                };
                var address = order.BillingAddress;
                if (address.Longitude.HasValue)
                {
                    feature.Geometry.Coordinates.Add(address.Longitude.Value);
                    feature.Geometry.Coordinates.Add(address.Latitude.Value);
                    feature.Geometry.Properties["city"] = order.BillingAddress.City;
                    yield return feature;
                }
            }

            static bool HasCoordinates(Order order)
            {
                return order.BillingAddress?.Latitude != null;
            }
        }
    }

    public class Feature
    {
        public string Type { get; set; } = "Feature";
        public string Id { get; set; }
        public Geometry Geometry { get; set; } = new Geometry();

    }

    public class Geometry
    {
        public string Type { get; set; } = "Point";
        public ICollection<decimal> Coordinates { get; set; } = new List<decimal>();
        public IDictionary<string,string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
