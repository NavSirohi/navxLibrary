using System.ComponentModel.DataAnnotations;

namespace LMSystem.Models
{
    public class LoginUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Student"; // "Admin", "Librarian", "Student"
    }
}