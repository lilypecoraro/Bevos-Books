using System.Collections.Generic;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Models.ViewModels
{
    public class CheckoutViewModel
    {
        // Order - nullable for model binding safety
        public Order? Order { get; set; }

        // Payment
        public int? SelectedCardID { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();

        // Coupon is OPTIONAL – make it nullable
        public string? CouponCode { get; set; }

        // Totals
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal OriginalSubtotal { get; set; }
        public decimal DiscountedSubtotal { get; set; }

    }
}