using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        public string Password { get; set; }
        public string SSN { get; set; }
        public string EmployeeType { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
