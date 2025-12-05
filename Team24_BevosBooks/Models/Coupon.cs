using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Coupon
    {
        [Key]
        public int CouponID { get; set; }

        [Required]
        [StringLength(20)]
        public string CouponCode { get; set; }

        [Required]
        [StringLength(20)]
        public string CouponType { get; set; }  // "FreeShipping" or "PercentOff"

        [Column(TypeName = "decimal(5,2)")]
        public decimal? DiscountPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? FreeThreshold { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Enabled"; // default value

        // Optional navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
