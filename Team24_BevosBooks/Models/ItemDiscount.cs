using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class ItemDiscount
    {
        [Key]
        public int ItemDiscountID { get; set; }

        [Required, ForeignKey("Book")]
        public int BookID { get; set; }
        public Book Book { get; set; }

        // Use either DiscountPercent OR DiscountAmount (flat). If both provided, percent wins.
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal? DiscountPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999)]
        public decimal? DiscountAmount { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } = "Enabled"; // "Enabled" | "Disabled"

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}