using Microsoft.AspNetCore.Mvc;

namespace ShopInsights.Web.Controllers
{
    [ApiController]
    [Route("api/ping")]
    public class PingController : Controller
    {
        // GET
        public ActionResult<int> Index()
        {
            return Ok(100);
        }
    }
}
