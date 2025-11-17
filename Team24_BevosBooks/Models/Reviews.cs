using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class Reviews
    {
        [Key]
        [Display(Name = "Review ID")]
        public int ReviewID { get; set; }

        // foreign keys
        [Required]
        [Display(Name = "Reviewer ID")]
        public int ReviewerID { get; set; }

        [Required]
        [Display(Name = "Book ID")]
        public int BookID { get; set; }

        [Required]
        [Display(Name = "Approver ID")]
        public int? ApproverID { get; set; }

        // Review-specific scalar properties
        [Required(ErrorMessage = "A rating is required.")]
        [Range(1, 5)]
        public int Rating { get; set; } // we can change this to be decimal if it says to in project specs


        [Display(Name = "Review Text")] // could change to just say "Review", depending on how it looks in view
        public string? ReviewText { get; set; } // leaving it optional unless otherwise specified in project specs


        [Display(Name = "DisputeStatus")]
        public string DisputeStatus { get; set; } // don't really know what this is. might be boolean type

        // Navigational properties

        // note, AppUser is the User class in the ERD. AppUser inherits from Identity template
        // further note, "Reviewer" is "ReviewerID". ID is automatically added to foreign key names by EF Core
        public AppUser Reviewer { get; set; }
        public Books Book { get; set; }
        public AppUser Approver { get; set; }

    }
}