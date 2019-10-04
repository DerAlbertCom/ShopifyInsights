using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Newtonsoft.Json;
using ShopifySharp;

namespace ShopInsights.Infrastructure.Services
{
    internal class AzureStore : IStore
    {
        private readonly IAzureTokenProvider _tokenProvider;

        public AzureStore(IAzureTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<bool> StoreOrders(IEnumerable<Order> orders)
        {
            var container = await GetBlobContainer();
            var blob = await container.GetBlobReferenceFromServerAsync("orders.json");

            var fileName = Path.GetTempFileName();
            try
            {
                using (var streamWriter = new StreamWriter(fileName))
                using (var writer = new JsonTextWriter(streamWriter))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, orders);
                }

                await blob.UploadFromFileAsync(fileName);
            }
            finally
            {
                File.Delete(fileName);
            }

            return true;
        }

        private async Task<CloudBlobContainer> GetBlobContainer()
        {
            var accessToken = await _tokenProvider.GetBlobStorageAccessTokenAsync();
            var tokenCredential = new TokenCredential(accessToken);
            var storageCredentials = new StorageCredentials(tokenCredential);

            var account = new CloudStorageAccount(storageCredentials, "reinland-seifen", "core.windows.net", true);

            var client = account.CreateCloudBlobClient();
            return client.GetContainerReference("shopdata");
        }
    }
}
