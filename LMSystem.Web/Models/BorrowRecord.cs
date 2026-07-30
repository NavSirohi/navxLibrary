using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class BorrowRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BorrowRecordId { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book? Book { get; set; }

        [Required]
        [StringLength(100)]
        public string? BorrowerName { get; set; }

        [Required]
        [EmailAddress]
        public string? BorrowerEmail { get; set; }

        [Required]
        [Phone]
        public string? Phone { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}