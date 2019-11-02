﻿using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Infrastructure.Stores
{
    public class OrderFilesImporter : FilesImporter<Order>, IOrderFilesImporter
    {
        public OrderFilesImporter(IOrderStorage storage, ILogger<OrderFilesImporter> logger):base(storage,"orders", logger)
        {

        }
    }
}
