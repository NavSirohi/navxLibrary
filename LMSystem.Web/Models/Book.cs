using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSystem.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Author { get; set; }

        [Required]
        [StringLength(20)]
        public string ISBN { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation to BorrowRecords (optional but useful)
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}