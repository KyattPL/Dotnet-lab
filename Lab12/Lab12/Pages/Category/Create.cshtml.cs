using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Lab12.Data;

namespace Lab12.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly MyDbContext _context;
        public string Name;

        public CreateModel(MyDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost([Bind("Id,Name")] Models.Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return RedirectToPage(nameof(OnGet));
        }
    }
}
