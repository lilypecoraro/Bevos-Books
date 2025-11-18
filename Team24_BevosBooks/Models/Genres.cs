using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }   // PK

        [Required, StringLength(100)]
        public string GenreName { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
