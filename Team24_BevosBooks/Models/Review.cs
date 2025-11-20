using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        // FK → Reviewer (AppUser)
        [Required]
        public string ReviewerID { get; set; }

        [ForeignKey("ReviewerID")]
        public AppUser Reviewer { get; set; }


        // FK → Book
        [Required]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        public Book Book { get; set; }


        // FK → Approver (AppUser)
        public string? ApproverID { get; set; }

        [ForeignKey("ApproverID")]
        public AppUser? Approver { get; set; }


        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? ReviewText { get; set; }

        [Required]
        public string DisputeStatus { get; set; }
    }
}
