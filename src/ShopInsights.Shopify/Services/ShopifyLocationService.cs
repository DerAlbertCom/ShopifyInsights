using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopifySharp;

namespace ShopInsights.Shopify.Services
{
    internal class ShopifyLocationService : IShopifyLocationService
    {
        private readonly LocationService _locationService;

        public ShopifyLocationService(LocationService locationService)
        {
            _locationService = locationService;
        }

        public async Task<IReadOnlyCollection<Location>> ListUpdatedSinceAsync()
        {
            var locations = await _locationService.ListAsync();
            return locations.ToArray();
        }
    }
}