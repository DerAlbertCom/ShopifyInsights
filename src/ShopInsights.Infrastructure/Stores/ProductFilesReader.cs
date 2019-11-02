using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ProductFilesReader : FilesReader<Product>, IProductFilesReader
    {
        public ProductFilesReader(IProductStorage storage, ILogger<ProductFilesReader> logger):base(storage,"products", logger)
        {

        }
    }
}