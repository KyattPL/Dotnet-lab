using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

using Lab12.Data;

namespace Lab12.Pages.Article
{
    public class CreateArticleViewModel : PageModel
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public readonly List<Models.Category> _categories;

        public int Id;
        public string Name;
        public string Price;
        public IFormFile? Picture;
        public int Category;

        public CreateArticleViewModel(MyDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _environment = hostEnvir;

            List<Models.Category> categories = _context.Category.ToList();
            _categories = categories;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost([Bind("Id,Name,Price,Picture,Category")] Models.ArticleCreateViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                string? combinedPath = null;

                if (articleVM.Picture != null && articleVM.Picture.Length > 0)
                {
                    string filename = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_" + articleVM.Picture.FileName;
                    combinedPath = Path.Combine("/upload/", filename);

                    using (Stream fileStream = new FileStream(_environment.WebRootPath + "/upload/" + filename, FileMode.Create))
                    {
                        await articleVM.Picture.CopyToAsync(fileStream);
                    }
                }

                Models.Article art = new Models.Article()
                {
                    Id = articleVM.Id,
                    Name = articleVM.Name,
                    Price = Convert.ToDecimal(articleVM.Price, new CultureInfo("en-US")),
                    Picture = combinedPath != null ? combinedPath : "/images/noImg.png",
                    Category = await _context.Category.FindAsync(articleVM.Category)
                };

                _context.Add(art);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
