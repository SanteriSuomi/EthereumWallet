using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EthereumWallet.Common.Networking.HTTP
{
    public class NetworkService : INetworkService
    {
        public NetworkService()
        {
            _httpClient = new HttpClient();
        }

        private readonly HttpClient _httpClient;

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(stringContent);
        }

        public async Task<TResult> GetAsync<TResult>(Uri uri)
        {
            var response = await _httpClient.GetAsync(uri);
            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResult>(stringContent);
        }
    }
}