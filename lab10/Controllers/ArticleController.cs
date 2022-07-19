#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab10.Data;
using lab10.Models;
using System.Globalization;
using System.IO;

namespace lab10.Controllers
{
    public class ArticleController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ArticleController(MyDbContext context, IWebHostEnvironment hostEnvir)
        {
            _context = context;
            _environment = hostEnvir;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Article.Include(a => a.Category).ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(article);
        }

        // GET: Articles/Create
        public async Task<IActionResult> Create()
        {
            List<Category> categories = await _context.Category.ToListAsync();
            ViewBag.Categories = categories;

            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Picture,Category")] ArticleCreateViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                //string uploadFolder = Path.Combine(_environment.WebRootPath, "upload");
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

                // Console.WriteLine(await _context.Category.FindAsync(articleVM.Category));

                Article art = new Article()
                {
                    Id = articleVM.Id,
                    Name = articleVM.Name,
                    Price = Convert.ToDecimal(articleVM.Price, new CultureInfo("en-US")),
                    Picture = combinedPath != null ? combinedPath : "/images/noImg.png",
                    Category = await _context.Category.FindAsync(articleVM.Category)
                };

                _context.Add(art);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            List<Category> categories = await _context.Category.ToListAsync();
            ViewBag.Categories = categories;

            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Picture,Category")] ArticleEditViewModel articleVM)
        {
            if (id != articleVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var art = new Article()
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
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Edit));
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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

            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
