using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models.ViewModels
{
    public class CardViewModel
    {
        [Required]
        [Display(Name = "Card Type")]
        public string CardType { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits.")]
        public string CardNumber { get; set; }
    }
}
