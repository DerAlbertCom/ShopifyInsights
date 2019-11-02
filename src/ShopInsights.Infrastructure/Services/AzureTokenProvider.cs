using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;

namespace ShopInsights.Infrastructure.Services
{
    public class AzureTokenProvider : IAzureTokenProvider
    {
        private readonly AzureServiceTokenProvider _tokenProvider = new AzureServiceTokenProvider();
        public async Task<string> GetDatabaseAccessTokenAsync()
        {
            return await _tokenProvider.GetAccessTokenAsync("https://database.windows.net/");
        }

        public async Task<string> GetBlobStorageAccessTokenAsync()
        {
            return await _tokenProvider.GetAccessTokenAsync("https://storage.azure.com/");
        }
    }
}
