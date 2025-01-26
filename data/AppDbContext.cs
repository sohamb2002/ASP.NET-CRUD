using asp_net_ecommerce_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_net_ecommerce_web_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
