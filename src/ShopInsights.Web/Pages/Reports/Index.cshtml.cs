using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopInsights.Core.Models;

namespace ShopInsights.Web.Pages.Reports
{
    public class Index : PageModel
    {
        public Index(IOrderStorage storage)
        {
            _storage = storage;
        }

        public void OnGet()
        {

        }

        private readonly IOrderStorage _storage;
    }
}
