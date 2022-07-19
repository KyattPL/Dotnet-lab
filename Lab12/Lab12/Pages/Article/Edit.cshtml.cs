using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using Lab12.Data;

namespace Lab12.Pages.Article
{
    public class EditModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public int Id;
        public string Name;
        public string Price;
        public string Picture;
        public int Category;
        public List<Models.Category> Categories;

        public EditModel(MyDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _environment = hostEnvir;

            Categories = _context.Category.ToList();
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

            Name = article.Name;
            Price = article.Price.ToString(new CultureInfo("en-US"));
            Picture = article.Picture;
            Category = article.Category.Id;
            Id = article.Id;

            return Page();
        }
        public async Task<IActionResult> OnPost(int id, [Bind("Id,Name,Price,Picture,Category")] Models.ArticleEditViewModel articleVM)
        {
            if (id != articleVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var art = new Models.Article()
                    {
                        Id = articleVM.Id,
                        Name = articleVM.Name,
                        Price = Convert.ToDecimal(articleVM.Price, new CultureInfo("en-US")),
                        Picture = articleVM.Picture,
                        Category = await _context.Category.FindAsync(articleVM.Category)
                    };

                    _context.Update(art);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(articleVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("./Index");
            }
            return Page();
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
