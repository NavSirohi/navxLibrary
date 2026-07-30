using System.Collections.Generic;
using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly LibraryContext _context;

        public StudentController(LibraryContext context)
        {
            _context = context;
        }

        // GET /Student
        public IActionResult Index()
        {
            var students = new List<StudentModel>();
            // Later you can load from _context.Students if you add DbSet<StudentModel>.
            return View(students);
        }

        // GET /Student/Create
        public IActionResult Create()
        {
            return View(new StudentModel());
        }

        // POST /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // If you later add DbSet<StudentModel> Students to LibraryContext,
            // you can save here. For now, just redirect to Index.
            // _context.Students.Add(model);
            // _context.SaveChanges();

            TempData["message"] = "Student created (demo only, not yet saved to DB).";
            return RedirectToAction("Index");
        }
    }
}