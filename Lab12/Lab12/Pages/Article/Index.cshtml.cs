using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

using Lab12.Data;

namespace Lab12.Pages.Article
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public readonly List<Models.Article> _articles;

        public int Id;
        public string Name;
        public string Price;
        public IFormFile? Picture;
        public int Category;

        public IndexModel(MyDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _environment = hostEnvir;

            _articles = _context.Article.Include(a => a.Category).ToList();
        }
        public void OnGet()
        {

        }
    }
}
