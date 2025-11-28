using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required, StringLength(100)]
        public string GenreName { get; set; }

        // Navigation property (must be nullable)
        public ICollection<Book>? Books { get; set; }
    }
}