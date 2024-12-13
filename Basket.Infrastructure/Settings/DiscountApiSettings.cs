using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Infrastructure.Settings
{
    public class DiscountApiSettings
    {
        public string BaseURL { get; set; }

        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
