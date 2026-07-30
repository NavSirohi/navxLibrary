using System;
using System;
using System.Linq;
using System.Threading.Tasks;
using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Web.Controllers
{
    public class PublicationsController : Controller
    {
        private readonly LibraryContext _context;

        public PublicationsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Publications/Index?type=Newspaper or Magazine
        public async Task<IActionResult> Index(string type, string? searchString, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest();
            }

            if (!Enum.TryParse(type, true, out PublicationType pubType))
            {
                return NotFound();
            }

            ViewData["CurrentType"] = type;
            ViewData["CurrentFilter"] = searchString;

            var items = _context.Publications
                .Where(p => p.Type == pubType)
                .AsQueryable();

            // Search [file:3]
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(p =>
                    p.Title.Contains(searchString) ||
                    p.Publisher.Contains(searchString));
            }

            int pageSize = 5;
            var totalItems = await items.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var paginatedList = await items
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewData["PageNumber"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(paginatedList);
        }

        // GET: Publications/Create?type=Newspaper or Magazine
        public IActionResult Create(string type)
        {
            ViewData["CurrentType"] = type;
            return View();
        }

        // POST: Publications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publication publication)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CurrentType"] = publication.Type.ToString();
                return View(publication);
            }

            _context.Add(publication);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { type = publication.Type.ToString() });
        }

        // GET: Publications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var publication = await _context.Publications.FindAsync(id);
            if (publication == null)
                return NotFound();

            ViewData["CurrentType"] = publication.Type.ToString();
            return View(publication);
        }

        // POST: Publications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Publication publication)
        {
            if (id != publication.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["CurrentType"] = publication.Type.ToString();
                return View(publication);
            }

            _context.Update(publication);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { type = publication.Type.ToString() });
        }

        // GET: Publications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var publication = await _context.Publications.FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
                return NotFound();

            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publication = await _context.Publications.FindAsync(id);
            if (publication != null)
            {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
            }

            var type = publication?.Type.ToString() ?? PublicationType.Newspaper.ToString();
            return RedirectToAction(nameof(Index), new { type });
        }
    }
}