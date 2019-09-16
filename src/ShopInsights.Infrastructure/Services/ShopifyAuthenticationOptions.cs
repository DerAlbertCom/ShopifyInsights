﻿namespace ShopInsights.Infrastructure.Services
{
    public class ShopifyAuthenticationOptions
    {
        public string ApiKey { get; set; }
        public string Password { get; set; }
        public string WebHookSecret { get; set; }
        public string ShopUrl { get; set; }
    }
}