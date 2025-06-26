

using Microsoft.EntityFrameworkCore;
using qlbb2.Entities;

namespace qlbb2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            // Đảm bảo database được tạo
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity properties and relationships here if needed
            base.OnModelCreating(modelBuilder);
        }

        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }
        // Add other DbSet properties as needed
    }
}
