using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Models
{
    public class ArticleEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Name { get; set; }
        [Required]
        public string Price { get; set; }
        public string Picture { get; set; }
        [Required]
        public int Category { get; set; }
    }
}
