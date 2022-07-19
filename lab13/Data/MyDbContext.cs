using lab10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace lab10.Data {
    public class MyDbContext : IdentityDbContext {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<lab10.Models.ArticleAPI> ArticleAPI { get; set; }
    }
}