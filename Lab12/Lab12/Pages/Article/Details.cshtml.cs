using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using Lab12.Data;

namespace Lab12.Pages.Article
{
    public class DetailsModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DetailsModel(MyDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _environment = hostEnvir;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            ViewData["artName"] = article.Name;
            ViewData["artPrice"] = article.Price.ToString(new CultureInfo("en-US"));
            ViewData["artPicture"] = article.Picture;
            ViewData["artCategoryName"] = article.Category.Name;
            ViewData["artId"] = article.Id;

            return Page();
        }
    }
}
