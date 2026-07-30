using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, string email, string message)
        {
            // In a real app, you would store or send this message.
            TempData["SuccessMessage"] = "Your message has been submitted.";
            return View();
        }
    }
}