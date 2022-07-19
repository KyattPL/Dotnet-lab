using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Lab12.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly MyDbContext _context;
        public readonly int Id;
        public string Name;

        public DeleteModel(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            Name = category.Name;
            return Page();
        }

        public async Task<IActionResult> OnPost(int? id)
        {
            var category = await _context.Category.FindAsync(id);
            var articles = await _context.Article.Include(a => a.Category).ToListAsync();

            foreach (var article in articles)
            {
                if (article.Category.Id == id)
                {
                    ViewData["ErrorMsg"] = "Cannot delete this category because it's connected to some articles!";
                    return RedirectToPage("Delete", category);
                }
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
