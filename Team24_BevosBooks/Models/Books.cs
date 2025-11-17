    public class Books
    {
        [Key]
        public int BookID { get; set; }  

        [ForeignKey("Genre")]
        public int GenreID { get; set; } 

        [Required]
        public int BookNumber { get; set; }  

        [Required, StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }   

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }    

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public int InventoryQuantity { get; set; }

        [Required]
        public int ReorderPoint { get; set; }

        [Required, StringLength(200)]
        public string Authors { get; set; }  

        [Required, StringLength(50)]
        public string BookStatus { get; set; }  

        public Genre Genre { get; set; }
    }
