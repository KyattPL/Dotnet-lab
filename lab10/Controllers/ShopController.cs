using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab10.Data;
using lab10.Models;

namespace lab10.Controllers
{
    public class ShopController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ShopController(MyDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _environment = hostEnvir;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Category.ToListAsync();
            List<Article> articles = await _context.Article.ToListAsync();
            
            ViewBag.Categories = categories;
            ViewBag.Articles = articles;
            ViewBag.NumOfCategories = categories.Count;

            return View();
        }

        [HttpPost]
        public void AddToBasket(int? id)
        {
            if (id != null)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(7);

                string? articleCountCookie = Request.Cookies[$"art{id.Value}"];
                string articleCount = articleCountCookie == null ? "0" : articleCountCookie;

                int countInt = Convert.ToInt32(articleCount) + 1;

                Response.Cookies.Append($"art{id.Value}", countInt.ToString(), option);
            }
        }
    }
}
