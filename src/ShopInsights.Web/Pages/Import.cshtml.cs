﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopInsights.Shopify.Models;
using ShopInsights.Shopify.Stores;

namespace ShopInsights.Web.Pages
{
    public class Import : PageModel
    {
        public Import(IShopifyOrderStorage shopifyOrderStorage, IShopifyOrderFilesReader reader)
        {
            _shopifyOrderStorage = shopifyOrderStorage;
            _reader = reader;
        }

        public void OnGet()
        {

        }

        public async Task OnPostImport(CancellationToken cancellationToken)
        {
     //       await _importer.ImportExistingOrdersAsync(cancellationToken);
        }

        public Task OnPostUpdate()
        {
            return Task.CompletedTask;
        }


        public bool ShowImport => !_shopifyOrderStorage.All.Any();

        private readonly IShopifyOrderStorage _shopifyOrderStorage;
        private readonly IShopifyOrderFilesReader _reader;
    }
}
