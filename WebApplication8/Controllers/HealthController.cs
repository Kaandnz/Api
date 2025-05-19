using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
