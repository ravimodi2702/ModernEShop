using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Application.HttpClient;
using Test.Core;

namespace Test.Infrastructure.HttpClient
{
    public class TestHttpClient : ITestHttpClient
    {
        private System.Net.Http.HttpClient _httpClient;
        public TestHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("httpclient");
        }
        public async Task<Student> GetAllStudent()
        {
            var httpResponse = await _httpClient.GetAsync("https://reqres.in/api/users/2");
            var response = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Student>(response);  
        }
    }
}
