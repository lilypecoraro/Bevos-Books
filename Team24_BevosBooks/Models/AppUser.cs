using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class AppUser : IdentityUser
    {
        // Inherits Id, UserName, PasswordHash, Email, PhoneNumber
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }


        public UserStatus Status { get; set; }

        // enum for user status
        public enum UserStatus
        {
            Customer,
            Employee,
            Admin
        }

        
    }
}