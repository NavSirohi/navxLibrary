using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Publication> Publications { get; set; }

        // New: users for login
        public DbSet<LoginUser> LoginUsers { get; set; }
    }
}