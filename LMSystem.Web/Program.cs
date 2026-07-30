using LMSystem.Data;
using LMSystem.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// EF Core InMemory
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseInMemoryDatabase("LibraryDb"));

var app = builder.Build();

// Seed default users into InMemory DB
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

    // Only seed once per app run
    if (!context.LoginUsers.Any())
    {
        context.LoginUsers.AddRange(
            new LoginUser
            {
                Username = "admin",
                Password = "12345",
                Role = "Admin"
            },
            new LoginUser
            {
                Username = "student1",
                Password = "stud123",
                Role = "Student"
            },
            new LoginUser
            {
                Username = "librarian1",
                Password = "lib123",
                Role = "Librarian"
            }
        );

        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();