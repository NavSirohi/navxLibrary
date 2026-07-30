using System;
using System.Linq;
using System.Threading.Tasks;
using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books (search + pagination)
        public async Task<IActionResult> Index(string? searchQuery, int page = 1)
        {
            try
            {
                int pageSize = 5; // rows per page [file:3]

                // 1. Base query with BorrowRecords included
                var booksQuery = _context.Books
                    .Include(b => b.BorrowRecords)
                    .AsNoTracking()
                    .AsQueryable();

                // 2. Apply search filter (Title, Author, ISBN) [file:3]
                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    searchQuery = searchQuery.Trim().ToLower();

                    booksQuery = booksQuery.Where(b =>
                        (b.Title != null && b.Title.ToLower().Contains(searchQuery)) ||
                        (b.Author != null && b.Author.ToLower().Contains(searchQuery)) ||
                        (b.ISBN != null && b.ISBN.ToLower().Contains(searchQuery)));
                }

                // 3. Count total items and compute total pages
                int totalItems = await booksQuery.CountAsync();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                if (page < 1) page = 1;
                if (totalPages > 0 && page > totalPages) page = totalPages;

                // 4. Apply Skip/Take for pagination [file:3]
                var books = await booksQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // 5. Build view model
                var viewModel = new BookListViewModel
                {
                    Books = books,
                    SearchQuery = searchQuery,
                    CurrentPage = page,
                    TotalPages = totalPages
                };

                return View(viewModel);
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while loading the books.";
                return View("Error");
            }
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided.";
                return View("NotFound");
            }

            try
            {
                var book = await _context.Books
                    .FirstOrDefaultAsync(m => m.BookId == id);

                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id}.";
                    return View("NotFound");
                }

                return View(book);
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while loading the book details.";
                return View("Error");
            }
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            try
            {
                // BookId and IsAvailable are set automatically [file:3]
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully added the book {book.Title}.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while adding the book.";
                return View(book);
            }
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for editing.";
                return View("NotFound");
            }

            try
            {
                var book = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.BookId == id);

                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for editing.";
                    return View("NotFound");
                }

                return View(book);
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while loading the book for editing.";
                return View("Error");
            }
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Book book)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for updating.";
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            try
            {
                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for updating.";
                    return View("NotFound");
                }

                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.PublishedDate = book.PublishedDate;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully updated the book {book.Title}.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "A concurrency error occurred during the update.";
                return View("Error");
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while updating the book.";
                return View("Error");
            }
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["ErrorMessage"] = "Book ID was not provided for deletion.";
                return View("NotFound");
            }

            try
            {
                var book = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.BookId == id);

                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                    return View("NotFound");
                }

                return View(book);
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while loading the book for deletion.";
                return View("Error");
            }
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    TempData["ErrorMessage"] = $"No book found with ID {id} for deletion.";
                    return View("NotFound");
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Successfully deleted the book {book.Title}.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the book.";
                return View("Error");
            }
        }
    }
}