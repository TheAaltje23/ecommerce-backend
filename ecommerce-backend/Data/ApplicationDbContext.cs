using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) => builder.HasPostgresEnum<User.Role>();
    }
}