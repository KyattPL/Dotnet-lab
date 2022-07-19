using System.ComponentModel.DataAnnotations;

namespace lab10.Models {

    public class Category {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage="Too short name!")]
        [MaxLength(30, ErrorMessage="Too long name!")]
        public string Name { get; set; }
    }

}