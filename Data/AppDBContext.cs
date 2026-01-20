using FruitSimulation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FruitSimulation.Data
{
    public class AppDBContext:IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
