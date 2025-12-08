using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Reorder
    {
        [Key]
        public int ReorderID { get; set; }   // PK

        [ForeignKey("Book")]
        public int BookID { get; set; }      // FK

        [Required, Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be greater than zero.")]
        public decimal Cost { get; set; }    // Supplier cost at reorder

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }    // Number of copies ordered

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }   // Reorder date

        [Required, StringLength(50)]
        public string ReorderStatus { get; set; }  // e.g., InCart, Ordered

        // Navigation property
        public Book Book { get; set; }
    }
}
