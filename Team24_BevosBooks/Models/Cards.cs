// filler file for project until github is setup

using System.ComponentModel.DataAnnotations;

//TODO: namespace [fill_in_here].Models
{
    public class Cards
{
    // Primary key
    [Key]
    [Display(Name = "Card ID")]
    public int CardID { get; set; }

    // foreign key
    [Required]
    [Display(Name = "Customer ID")]
    public int CustomerID { get; set; }

    // Review-specific scalar properties
    [Display(Name = "Card Number")]
    public int CardNumber { get; set; }

    [Display(Name = "Card Type")]
    public enum CardTypes CardType { get; set; }

    
    // Navigational properties

    // note, AppUser is the User class in the ERD. AppUser inherits from Identity template
    // further note, "Customer" is "CustomerID". ID is automatically added to foreign key names by EF Core
    public AppUser Customer { get; set; }
   
    // enum
    public enum CardTypes
    {
        Visa,
        MasterCard,
        AmericanExpress,
        Discover

    }
}
