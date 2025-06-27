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
            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Role).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }
        // Add other DbSet properties as needed
    }
}
