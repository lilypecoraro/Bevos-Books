using System.Collections.Generic;
using Team24_BevosBooks.Models;

namespace Team24_BevosBooks.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }

        // Payment
        public int? SelectedCardID { get; set; }
        public List<Card> Cards { get; set; }

        // Coupon
        public string CouponCode { get; set; }

        // Totals
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
    }
}
