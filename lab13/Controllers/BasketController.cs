using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab10.Models;
using lab10.Data;

namespace lab10.Controllers
{
    public class BasketController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BasketController(MyDbContext context, IWebHostEnvironment environ)
        {
            _context = context;
            _environment = environ;
        }

        public async Task<IActionResult> Index()
        {
            var cookieKeys = Request.Cookies.Keys;
            List<int> articleIds = new List<int>();
            List<int> articleCounts = new List<int>();

            List<string> unorderedArts = new List<string>();

            foreach (var key in cookieKeys)
            {
                if (key.StartsWith("art"))
                {
                    unorderedArts.Add(key);
                }
            }

            unorderedArts.Sort();

            foreach (var key in unorderedArts)
            {
                articleIds.Add(Convert.ToInt32(key.Remove(0, 3)));
                articleCounts.Add(Convert.ToInt32(Request.Cookies[key]));
            }

            List<Article> articles = new List<Article>();

            foreach (var id in articleIds)
            {
                Article? art = await _context.Article.Include(a => a.Category).FirstAsync(a => a.Id == id);
                if (art != null)
                {
                    articles.Add(art);
                }
            }

            decimal total = 0;
            int index = 0;
            foreach (var article in articles)
            {
                total += article.Price * articleCounts[index];
                index += 1;
            }

            ViewBag.Total = total;
            ViewBag.Articles = articles;
            ViewBag.ArtCounts = articleCounts;
            ViewBag.OrderedKeys = unorderedArts;

            return View();
        }

        public IActionResult AddArticle(string? id)
        {
            var cookieKeys = Request.Cookies.Keys;
            string? cookieCountKey = null;

            foreach (var key in cookieKeys)
            {
                if (id != null && key == id)
                {
                    cookieCountKey = key;
                }
            }

            if (cookieCountKey != null)
            {
                int howManyCookies = Convert.ToInt32(Request.Cookies[cookieCountKey]);
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append(cookieCountKey, (howManyCookies + 1).ToString(), option);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult TakeArticle(string? id)
        {
            var cookieKeys = Request.Cookies.Keys;
            string? cookieCountKey = null;

            foreach (var key in cookieKeys)
            {
                if (id != null && key == id)
                {
                    cookieCountKey = key;
                }
            }

            if (cookieCountKey != null)
            {
                int howManyCookies = Convert.ToInt32(Request.Cookies[cookieCountKey]);
                if (howManyCookies > 1)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Append(cookieCountKey, (howManyCookies - 1).ToString(), option);
                }
                else
                {
                    Response.Cookies.Delete(cookieCountKey);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveArticle(string? id)
        {
            var cookieKeys = Request.Cookies.Keys;
            string? cookieCountKey = null;

            foreach (var key in cookieKeys)
            {
                if (id != null && key == id)
                {
                    cookieCountKey = key;
                }
            }

            if (cookieCountKey != null)
            {
                Response.Cookies.Delete(cookieCountKey);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OrderArticles()
        {
            var cookieKeys = Request.Cookies.Keys;
            List<int> articleIds = new List<int>();
            List<int> articleCounts = new List<int>();

            List<string> unorderedArts = new List<string>();

            foreach (var key in cookieKeys)
            {
                if (key.StartsWith("art"))
                {
                    unorderedArts.Add(key);
                }
            }

            unorderedArts.Sort();

            foreach (var key in unorderedArts)
            {
                articleIds.Add(Convert.ToInt32(key.Remove(0, 3)));
                articleCounts.Add(Convert.ToInt32(Request.Cookies[key]));
            }

            List<Article> articles = new List<Article>();

            foreach (var id in articleIds)
            {
                Article? art = await _context.Article.Include(a => a.Category).FirstAsync(a => a.Id == id);
                if (art != null)
                {
                    articles.Add(art);
                }
            }

            decimal total = 0;
            int index = 0;
            foreach (var article in articles)
            {
                total += article.Price * articleCounts[index];
                index += 1;
            }

            ViewBag.Total = total;
            ViewBag.Articles = articles;
            ViewBag.ArtCounts = articleCounts;
            ViewBag.OrderedKeys = unorderedArts;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderArticles([Bind("FirstName,LastName,FirstAddress,SecondAddress,PhoneNumber,PaymentMethod")] CustomerInfo info)
        {
            if (ModelState.IsValid)
            {
                ViewBag.FirstName = info.FirstName;
                ViewBag.LastName = info.LastName;
                ViewBag.FirstAddress = info.FirstAddress;
                ViewBag.SecondAddress = info.SecondAddress;
                ViewBag.PhoneNumber = info.PhoneNumber;
                ViewBag.PaymentMethod = info.PaymentMethod;

                var cookieKeys = Request.Cookies.Keys;

                foreach (var key in cookieKeys)
                {
                    if (key.StartsWith("art"))
                    {
                        Response.Cookies.Delete(key);
                    }
                }

                return View("Summary");
            } else
            {
                var cookieKeys = Request.Cookies.Keys;
                List<int> articleIds = new List<int>();
                List<int> articleCounts = new List<int>();

                List<string> unorderedArts = new List<string>();

                foreach (var key in cookieKeys)
                {
                    if (key.StartsWith("art"))
                    {
                        unorderedArts.Add(key);
                    }
                }

                unorderedArts.Sort();

                foreach (var key in unorderedArts)
                {
                    articleIds.Add(Convert.ToInt32(key.Remove(0, 3)));
                    articleCounts.Add(Convert.ToInt32(Request.Cookies[key]));
                }

                List<Article> articles = new List<Article>();

                foreach (var id in articleIds)
                {
                    Article? art = await _context.Article.Include(a => a.Category).FirstAsync(a => a.Id == id);
                    if (art != null)
                    {
                        articles.Add(art);
                    }
                }

                decimal total = 0;
                int index = 0;
                foreach (var article in articles)
                {
                    total += article.Price * articleCounts[index];
                    index += 1;
                }

                ViewBag.Total = total;
                ViewBag.Articles = articles;
                ViewBag.ArtCounts = articleCounts;
                ViewBag.OrderedKeys = unorderedArts;

                return View(nameof(OrderArticles));
            }
        }
    }
}
