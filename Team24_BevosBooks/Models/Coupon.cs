using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BevosBooks.Models
{
    public class Coupon
    {
        [Key]
        public int CouponID { get; set; }   // PK

        [Required]
        [StringLength(20)]                  // 1–20 characters, letters/numbers
        public string CouponCode { get; set; }

        [Required]
        [StringLength(20)]
        public string CouponType { get; set; }  
        // "FreeShipping" or "PercentOff"

        [Column(TypeName = "decimal(5,2)")]
        public decimal? DiscountPercent { get; set; }  
        // Only used for PercentOff type (e.g., 10.00 = 10%)

        [Column(TypeName = "decimal(18,2)")]
        public decimal? FreeThreshold { get; set; }    
        // Minimum order amount for free shipping (nullable)

        [Required]
        [StringLength(20)]
        public string Status { get; set; }  
        // "Enabled" or "Disabled"

        // Navigation property (optional, if you want to track usage in orders)
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
