using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BevosBooks.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }   // PK

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }  // FK to User (Customer)

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; }

        [Required, StringLength(50)]
        public string OrderStatus { get; set; }  // e.g., Placed, Shipped, Cancelled

        // Navigation properties
        public User Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
