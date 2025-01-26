using Microsoft.EntityFrameworkCore;
using asp_net_ecommerce_web_api.Models;

namespace asp_net_ecommerce_web_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations (if needed)
        }
    }
}
