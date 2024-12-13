using Basket.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Basket.Infrastructure.HttpHandlers.DiscountApiHandler
{

    public class DiscountApiHttpHandler : DelegatingHandler
    {
        private readonly DiscountApiSettings _discountsSettings;
        private readonly ILogger<DiscountApiHttpHandler> _logger;

        public DiscountApiHttpHandler(IOptions<DiscountApiSettings> discountApiSettings, ILogger<DiscountApiHttpHandler> logger)
        {

            _discountsSettings = discountApiSettings.Value;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("call started");

            var authToken = await GetAccessTokenAsync(cancellationToken);


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var httpResponseMessage = await base.SendAsync(
                request,
                cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }


        private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            return "token";
            /*var params = new KeyValuePair<string, string>[]
            {
            new("client_id", _discountsSettings.ClientId),
            new("client_secret", _discountsSettings.ClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
            };

            var content = new FormUrlEncodedContent(params);

            var authRequest = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(_discountsSettings.TokenUrl))
            {
                Content = content
            };

            var response = await base.SendAsync(authRequest, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<string>() ??
                   throw new System.Exception();*/
        }
    }
}
