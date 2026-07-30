using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Uses Views/About/About.cshtml
        public IActionResult About()
        {
            return View("/Views/About/About.cshtml");
        }

        // Uses Views/Contact/Contact.cshtml
        public IActionResult Contact()
        {
            return View("/Views/Contact/Contact.cshtml");
        }
    }
}