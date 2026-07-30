using System.Linq;
using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LibraryContext _context;

        public LoginController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public IActionResult Verify(LoginModel usr)
        {
            var user = _context.LoginUsers
                .FirstOrDefault(u => u.Username == usr.Username && u.Password == usr.Password);

            if (user == null)
            {
                ViewBag.message = "Login Failed";
                return View("Index", usr);
            }

            TempData["LoggedInUser"] = user.Username;
            TempData["LoggedInRole"] = user.Role;
            TempData["message"] = $"Login Success ({user.Role})";

            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if (user.Role == "Librarian")
            {
                return RedirectToAction("Index", "Librarian");
            }
            else
            {
                return RedirectToAction("Index", "Student");
            }
        }

        public IActionResult Logout()
        {
            TempData["LoggedInUser"] = null;
            TempData["LoggedInRole"] = null;
            TempData["message"] = "You have been logged out.";
            return RedirectToAction("Index");
        }
    }
}