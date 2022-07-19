using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab12.Data;

namespace Lab12.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;
        public readonly List<Models.Category> categories;

        public IndexModel(MyDbContext context)
        {
            _context = context;
            categories = _context.Category.ToList();
        }
        public void OnGet()
        {
            
        }
    }
}
