using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.HttpClients
{
    public class MockAPIHttpHandler : DelegatingHandler
    {
        private readonly ILogger<MockAPIHttpHandler> _logger;
        public MockAPIHttpHandler(ILogger<MockAPIHttpHandler> logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"MockAPIHttpHandler: {request}");
            string authToken = "authtoken";
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            var res = await base.SendAsync(request, cancellationToken);
            return res;
        }
    }
}
