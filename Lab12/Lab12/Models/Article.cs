using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models {

    public class Article {

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [MaxLength(75)]
        public string Picture { get; set; }
        [Required]
        public Category Category { get; set; }
    }
}