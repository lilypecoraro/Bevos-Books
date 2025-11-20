using System;
using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required] public string Password { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string FirstName { get; set; }

        public DateTime Birthday { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
