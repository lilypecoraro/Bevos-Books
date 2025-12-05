using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Team24_BevosBooks.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }   // PK

        [ForeignKey("Genre")]
        public int GenreID { get; set; }  // FK

        [Required]
        public int BookNumber { get; set; }  // Unique consecutive number (e.g., 222001+)

        [Required, StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }   // Selling price

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }    // Supplier cost

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public int InventoryQuantity { get; set; }

        [Required]
        public int ReorderPoint { get; set; }

        [Required, StringLength(200)]
        public string Authors { get; set; }  // Simplified as string list

        [Required, StringLength(50)]
        public string BookStatus { get; set; }  // e.g., Active, Discontinued

        // Navigation property
        public Genre Genre { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        // ✅ Computed property for average profit margin
        [NotMapped] // ensures EF doesn’t try to persist this
        public decimal AvgProfitMargin
        {
            get
            {
                if (Price <= 0) return 0;
                return (Price - Cost) / Price;
            }
        }
    }
}
