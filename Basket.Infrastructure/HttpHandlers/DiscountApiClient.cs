using Basket.Application.IHttpHandlers;
using Basket.Infrastructure.Settings;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Basket.Infrastructure.HttpHandlers
{
    public class DiscountApiClient : IDiscountApiClient
    {
        private IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public DiscountApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("discountApiClient");

        }

        public async Task<int> GetDiscount(string productName)
        {
            var response = await _httpClient.GetAsync("https://api.example.com/data");
            var responseBody = await response.Content.ReadAsStringAsync();
            var a = JsonConvert.DeserializeObject<object>(responseBody);
            _httpClient.Dispose();
            await Task.Delay(1000);
            return 10;
        }
    }
}
