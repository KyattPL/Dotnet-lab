using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace lab10.Models {

    public class Article {

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [MaxLength(40)]
        public string Picture { get; set; }
        [Required]
        public Category Category { get; set; }
    }
}