using Microsoft.EntityFrameworkCore;
using qlbb2.Data.Entities;

namespace qlbb2.Infrastructure
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

            // Seed default admin user
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "admin",
                Password = "123", // Nên hash mật khẩu nếu dùng thực tế
                Role = "Admin"
            });

            // Configure Supplier entity
            modelBuilder.Entity<TblSupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId);
                entity.Property(e => e.SupplierName).IsRequired();
                entity.Property(e => e.Phone).IsRequired(false);
                entity.Property(e => e.Email).IsRequired(false);
                entity.Property(e => e.Address).IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }

        // Define DbSet properties for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<TblSupplier> Suppliers { get; set; }
        // Add other DbSet properties as needed

    }
}
