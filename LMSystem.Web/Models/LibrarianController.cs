using System.Collections.Generic;
using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly LibraryContext _context;

        public LibrarianController(LibraryContext context)
        {
            _context = context;
        }

        // GET /Librarian
        public IActionResult Index()
        {
            // For now, show an empty list. Later you can store librarians in InMemory DB.
            var librarians = new List<LibrarianModel>();

            return View(librarians);
        }

        // Later you can add Create/Edit/Delete actions that use _context
    }
}