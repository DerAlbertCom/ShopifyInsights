using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class MetaFieldFileWriter : FilesWriter<MetaField>, IMetaFieldFilesWriter
    {
        public MetaFieldFileWriter(IMetaFieldStorage storage, ILogger<MetaFieldFileWriter> logger) : base(storage, "metafields", logger)
        {
        }
    }
}