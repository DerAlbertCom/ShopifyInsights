﻿namespace ShopInsights.Shopify.Services.Shopify
{
    public class ShopifyOptions
    {
        public string ApiKey { get; set; }
        public string Password { get; set; }
        public string WebHookSecret { get; set; }
        public string ShopUrl { get; set; }

        public bool FetchNewData { get; set; }
    }
}
