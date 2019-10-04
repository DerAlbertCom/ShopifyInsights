using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopInsights.Core.Models;
using ShopInsights.Core.Stores;

namespace ShopInsights.Web.Pages
{
    public class Import : PageModel
    {
        public Import(IOrderStorage orderStorage, IOrderFilesImporter importer)
        {
            _orderStorage = orderStorage;
            _importer = importer;
        }

        public void OnGet()
        {

        }

        public async Task OnPostImport()
        {
            await _importer.ImportExistingOrdersAsync();
        }

        public Task OnPostUpdate()
        {
            return Task.CompletedTask;
        }


        public bool ShowImport => !_orderStorage.AllOrders.Any();

        private readonly IOrderStorage _orderStorage;
        private readonly IOrderFilesImporter _importer;
    }
}
