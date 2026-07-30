using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LMSystem.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly LibraryContext _context;

        public DashboardController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new DashboardModel
            {
                // If you haven't wired Students/Librarians to EF yet, these will be 0 for now.
                TotalStudents = 0,
                TotalLibrarians = 0,
                TotalBooks = _context.Books.Count(),
                TotalBorrowings = _context.BorrowRecords.Count(),
                TotalPublications = _context.Publications.Count()
            };

            // Show who is logged in (from TempData set in LoginController)
            ViewBag.LoggedInUser = TempData["LoggedInUser"];
            ViewBag.LoggedInRole = TempData["LoggedInRole"];

            return View(model);
        }
    }
}