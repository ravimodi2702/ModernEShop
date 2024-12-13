using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.HttpClients
{
    public class MockApiSettings
    {
        public string BaseUrl { get; set; }
        public string GetUrl { get; set; }
        public string PostUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
