﻿using System.Threading.Tasks;

 namespace ShopInsights.Infrastructure.Services
{
    public interface IAzureTokenProvider
    {
        Task<string> GetDatabaseAccessTokenAsync();
        Task<string> GetBlobStorageAccessTokenAsync();
    }
}