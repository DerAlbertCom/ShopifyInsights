using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class MetaFieldFilesReader : FilesReader<MetaField>, IMetaFieldFilesReader
    {
        public MetaFieldFilesReader(IMetaFieldStorage storage, ILogger<MetaFieldFilesReader> logger):base(storage,"metafields", logger)
        {

        }
    }
}
