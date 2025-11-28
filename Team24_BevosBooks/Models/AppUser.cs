using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class AppUser : IdentityUser
    {
        [Required, StringLength(40)]
        public string FirstName { get; set; }

        [Required, StringLength(40)]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required, StringLength(2)]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public UserStatus Status { get; set; } = UserStatus.Customer;

        public enum UserStatus
        {
            Customer,
            Employee,
            Admin,
            Disabled   
        }
        // Not stored in the database – used only for viewing roles in ManageEmployees
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IList<string> RoleNames { get; set; } = new List<string>();
    }
}