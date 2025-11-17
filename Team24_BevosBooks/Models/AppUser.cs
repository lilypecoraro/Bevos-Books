// filler file for project until github is setup

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

//TODO: namespace [fill_in_here].Models
{
    public class AppUser : IdentityUser
    {
        // already includes Id, UserName, PasswordHash, Email, PhoneNumber

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public enum UserStatus Status { get; set; } // I believe this is their roles (Admin, Employee, Customer)

    // enum
    public enum UserStatus
        {
            Customer,
            Employee,
            Admin
    }

}
}
