using Microsoft.AspNetCore.Mvc;

namespace eVillageCinemas.Controllers
{
    public class HealthCheckController : Controller
    {
        public IActionResult Index()
        {
            return Json(new { status = "OK" });
        }
    }
}
