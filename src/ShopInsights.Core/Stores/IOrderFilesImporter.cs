﻿using System.Threading.Tasks;

namespace ShopInsights.Core.Stores
{
    public interface IOrderFilesImporter
    {
        Task ImportExistingOrdersAsync();
    }
}
