using System.ComponentModel.DataAnnotations;

namespace lab10.Models
{
    public class CustomerInfo
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string FirstAddress { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string SecondAddress { get; set; }
        [Required]
        [StringLength(9)]
        [RegularExpression(@"^(\d{9})$")]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(40)]
        public string PaymentMethod { get; set; }
    }
}
