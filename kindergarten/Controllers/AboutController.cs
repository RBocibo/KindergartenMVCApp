using Microsoft.AspNetCore.Mvc;

namespace kindergarten.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
