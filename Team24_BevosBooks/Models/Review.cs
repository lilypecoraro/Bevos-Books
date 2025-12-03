using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team24_BevosBooks.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        // =========================
        // Foreign Keys
        // =========================
        [Required]
        public int BookID { get; set; }
        public Book Book { get; set; }

        [Required]
        public string ReviewerID { get; set; }
        public AppUser Reviewer { get; set; }

        public string? ApproverID { get; set; }
        public AppUser? Approver { get; set; }

        // =========================
        // Review Data
        // =========================
        [Required(ErrorMessage = "Please select a rating.")]
        [Range(1, 5, ErrorMessage = "Please select a rating.")]
        public int? Rating { get; set; } // needed to add ? for proper error message to show. Rating will never actually take on a null value due to required tag

        [Required(ErrorMessage = "The review field cannot be blank.")]
        [StringLength(100)]
        public string ReviewText { get; set; }

        // "Pending", "Approved", "Rejected"
        [Required]
        [StringLength(20)]
        public string DisputeStatus { get; set; } = "Pending";

    }
}
