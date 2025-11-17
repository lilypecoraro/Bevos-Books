    public class Reorder
    {
        [Key]
        public int ReorderID { get; set; }   

        [ForeignKey("Book")]
        public int BookID { get; set; }   

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }   

        [Required]
        public int Quantity { get; set; }  

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } 

        [Required, StringLength(50)]
        public string ReorderStatus { get; set; }  

        public Book Book { get; set; }
    }
