using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Reorders
    {
        [Key]
        public int ReorderID { get; set; }   // PK

        [ForeignKey("Book")]
        public int BookID { get; set; }      // FK

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }    // Supplier cost at reorder

        [Required]
        public int Quantity { get; set; }    // Number of copies ordered

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }   // Reorder date

        [Required, StringLength(50)]
        public string ReorderStatus { get; set; }  // e.g., Pending, Completed

        // Navigation property
        public Book Book { get; set; }
    }
}
