using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Lab12.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab12.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly MyDbContext _context;
        public int Id;
        public string Name;

        public DetailsModel(MyDbContext context)
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

            Id = category.Id;
            Name = category.Name;
            return Page();
        }
    }
}
