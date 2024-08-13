using Microsoft.EntityFrameworkCore;
using ecommerce_backend.Models;

namespace ecommerce_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> UserData { get; set; }
    }
}