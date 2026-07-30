using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}