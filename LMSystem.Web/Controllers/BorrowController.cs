using System.Collections.Generic;
using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{
    public class BorrowController : Controller
    {
        private readonly LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        // GET /Borrow
        public IActionResult Index()
        {
            // For now, use an empty list or later load from _context.BorrowRecords.
            var records = new List<BorrowRecord>();

            // Example: if you have DbSet<BorrowRecord> BorrowRecords in LibraryContext,
            // you could do: var records = _context.BorrowRecords.Include(b => b.Book).ToList();

            return View(records);
        }
    }
}