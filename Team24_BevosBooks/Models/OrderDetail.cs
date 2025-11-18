using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailsID { get; set; }   // PK

        [ForeignKey("Order")]
        public int OrderID { get; set; }          // FK to Order

        [ForeignKey("Book")]
        public int BookID { get; set; }           // FK to Book

        [ForeignKey("Card")]
        public int CardID { get; set; }           // FK to Card used for payment

        [ForeignKey("Coupon")]
        public int? CouponID { get; set; }        // Optional FK to Coupon

        [Required]
        public int Quantity { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }        // Selling price at time of order

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }         // Supplier cost at time of order

        // Navigation properties
        public Order Order { get; set; }
        public Book Book { get; set; }
        public Card Card { get; set; }
        public Coupon Coupon { get; set; }
    }
}
