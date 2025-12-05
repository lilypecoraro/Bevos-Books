using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }   // AppUser Id

        public AppUser User { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [ForeignKey("Coupon")]
        public int? CouponID { get; set; }   // FK to Coupon (nullable if no coupon used)

        public Coupon Coupon { get; set; }

    }
}