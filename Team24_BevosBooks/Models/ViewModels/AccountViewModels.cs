using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required, Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, StringLength(40)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // REQUIRED BY SPEC AND YOUR CONTROLLER
        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required, StringLength(2)]
        public string State { get; set; }

        [Required, Display(Name = "ZIP Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Please enter a standardized ZIP code.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter a password.")] 
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public class ChangePasswordViewModel
    {
        [Required, DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required, StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
