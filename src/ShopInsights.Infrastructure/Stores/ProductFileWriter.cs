using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class ProductFileWriter : FilesWriter<Product>, IProductFilesWriter
    {
        public ProductFileWriter(IProductStorage storage, ILogger<ProductFileWriter> logger) : base(storage, "products", logger)
        {
        }
    }
}
