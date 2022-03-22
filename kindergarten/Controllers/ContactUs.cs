using Microsoft.AspNetCore.Mvc;

namespace kindergarten.Controllers
{
    public class ContactUs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
