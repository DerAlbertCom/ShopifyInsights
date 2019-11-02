using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ProductFileStorage : FilesStorage<Product>, IProductFilesStorage
    {
        public ProductFileStorage(IProductStorage storage, ILogger<ProductFileStorage> logger) : base(storage, "products", logger)
        {
        }
    }
}
