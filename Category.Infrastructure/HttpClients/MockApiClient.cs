using Catalog.Application.HttpClients;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace Catalog.Infrastructure.HttpClients
{
    public class MockApiClient : IMockApiClient
    {
        private readonly HttpClient _httpClient;
        public MockApiClient(IHttpClientFactory httpClientFactory, IOptions<MockApiSettings> mockApiSettings)
        {
            _httpClient = httpClientFactory.CreateClient("mockapi");
        }

        public async Task<MockAPiResponse> GetData(string param)
        {
            var url = new Uri($"{ _httpClient.BaseAddress.ToString() }users/{ param}");
            var httpResponse = await _httpClient.GetAsync(url);
            var response = await httpResponse.Content.ReadAsStringAsync();
            var mockResponse = JsonConvert.DeserializeObject<MockAPiResponse>(response);
            return mockResponse;
        }

        public async Task<MockPostResponse> PostData(MockReq mockReq)
        {
            var url = new Uri($"{_httpClient.BaseAddress}users");
            var requestPayload = JsonConvert.SerializeObject(mockReq);
            var httpResponse = await _httpClient.PostAsync(url, new  StringContent(requestPayload));
            var response = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MockPostResponse>(response);            
        }
    }
}
