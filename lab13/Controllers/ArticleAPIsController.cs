#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab10.Data;
using lab10.Models;

namespace lab10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleAPIsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ArticleAPIsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/ArticleAPIs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticleAPI()
        {
            return await _context.Article.Include(c => c.Category).ToListAsync();
        }

        // GET: api/ArticleAPIs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticleAPI(int id)
        {
            var articleAPI = await _context.Article.Include(c => c.Category).FirstOrDefaultAsync();

            if (articleAPI == null)
            {
                return NotFound();
            }

            return articleAPI;
        }

        // PUT: api/ArticleAPIs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleAPI(int id, ArticleAPI articleAPI)
        {
            if (id != articleAPI.Id)
            {
                return BadRequest();
            }

            Article art = new Article
            {
                Id = articleAPI.Id,
                Name = articleAPI.Name,
                Price = articleAPI.Price,
                Category = await _context.Category.FindAsync(articleAPI.Category),
                Picture = "/images/noImg.png"
            };

            _context.Entry(art).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleAPIExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ArticleAPIs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticleAPI(ArticleAPI articleAPI)
        {
            Article art = new Article { 
                Id = articleAPI.Id,
                Name = articleAPI.Name,
                Price = articleAPI.Price,
                Category = await _context.Category.FindAsync(articleAPI.Category),
                Picture = "/images/noImg.png"
            };
            _context.Article.Add(art);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticleAPI", new { id = articleAPI.Id }, articleAPI);
        }

        // DELETE: api/ArticleAPIs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleAPI(int id)
        {
            var articleAPI = await _context.Article.FindAsync(id);
            if (articleAPI == null)
            {
                return NotFound();
            }

            _context.Article.Remove(articleAPI);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("next/{num}")]
        public async Task<ActionResult<IEnumerable<Article>>> GetNextArts(int num)
        {
            return await _context.Article.Include(a => a.Category).Skip(num).Take(3).ToListAsync();
        }

        private bool ArticleAPIExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
