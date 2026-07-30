using System;
using System.ComponentModel.DataAnnotations;

namespace LMSystem.Models
{


    public class Publication
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(50)]
        public string? Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [Required]
        public PublicationType Type { get; set; }  // Newspaper vs Magazine

        public bool IsAvailable { get; set; } = true;
    }
}