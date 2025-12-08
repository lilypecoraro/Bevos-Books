using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Card ID")]
        public int CardID { get; set; }

        // FK → AppUser
        [Required]
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }   // AppUser.Id is string

        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(80)]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        [MaxLength(30)]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        public CardTypes CardType { get; set; }

        // Navigation
        public AppUser User { get; set; }

        public enum CardTypes
        {
            Visa,
            MasterCard,
            AmericanExpress,
            Discover
        }
    }
}
