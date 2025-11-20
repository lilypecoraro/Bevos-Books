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

        // FK to AppUser (the customer placing the order)
        [ForeignKey("User")]
        public string UserID { get; set; }   // AppUser.Id is a string

        public AppUser User { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        // Navigation to details
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
