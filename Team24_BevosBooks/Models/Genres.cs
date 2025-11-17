
    public class Genres
    {
        [Key]
        public int GenreID { get; set; }   

        [Required, StringLength(100)]
        public string GenreName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
