using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ProductFilesImporter : FilesImporter<Product>, IProductFilesImporter
    {
        public ProductFilesImporter(IProductStorage storage, ILogger<ProductFilesImporter> logger):base(storage,"products", logger)
        {

        }
    }
}