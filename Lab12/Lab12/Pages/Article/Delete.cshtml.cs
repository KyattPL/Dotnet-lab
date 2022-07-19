using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using Lab12.Data;

namespace Lab12.Pages.Article
{
    public class DeleteModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeleteModel(MyDbContext context, IWebHostEnvironment hostEnvir)
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

        public async Task<IActionResult> OnPost(int id)
        {
            var article = await _context.Article.FindAsync(id);

            var imagePath = article.Picture;
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            if (imagePath != "/images/noImg.png")
            {
                string t3 = _environment.WebRootPath + imagePath.Replace('/', '\\');
                System.IO.File.Delete(t3);
            }

            return RedirectToPage("./Index");
        }
    }
}
