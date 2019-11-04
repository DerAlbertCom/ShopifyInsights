using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ShopifyProductFilesReader : ShopifyFilesReader<Product>, IShopifyProductFilesReader
    {
        public ShopifyProductFilesReader(IShopifyProductStorage storage, ILogger<ShopifyProductFilesReader> logger):base(storage,"products", logger)
        {

        }
    }
}
