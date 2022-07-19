using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12.Data;

namespace Lab12.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public int NumOfCategories;
        public List<Models.Article> Articles;
        public List<Models.Category> Categories;

        public IndexModel(MyDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> OnGet()
        {
            List<Models.Category> categories = await _context.Category.ToListAsync();
            List<Models.Article> articles = await _context.Article.ToListAsync();

            Categories = categories;
            Articles = articles;
            NumOfCategories = categories.Count;

            return Page();
        }
    }
}
