using Microsoft.EntityFrameworkCore;
using Lab12.Models;

namespace Lab12.Data {
    public class MyDbContext : DbContext {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }
    }
}