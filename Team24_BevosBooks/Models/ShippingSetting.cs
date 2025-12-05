using System.ComponentModel.DataAnnotations;

namespace Team24_BevosBooks.Models
{
    public class ShippingSetting
    {
        [Key]
        public int SettingID { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal FirstBookRate { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal AdditionalBookRate { get; set; }
    }
}
