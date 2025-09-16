using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BE.Infrastructure.Database
{
    public class BEContext : DbContext
    {
        public BEContext(DbContextOptions<BEContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<SupplierProduct> SupplierProducts => Set<SupplierProduct>();
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Supplier>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<SupplierProduct>(b =>
            {
                b.HasKey(x => x.Id);

                b.HasOne(sp => sp.Product)
                 .WithMany(p => p.SupplierProducts)
                 .HasForeignKey(sp => sp.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(sp => sp.Supplier)
                 .WithMany(s => s.SupplierProducts)
                 .HasForeignKey(sp => sp.SupplierId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.Property(sp => sp.Price).HasColumnType("decimal(18,2)");
                b.Property(sp => sp.Stock).IsRequired();

                b.HasIndex(sp => new { sp.ProductId, sp.SupplierId, sp.LotNumber }).IsUnique(false);
            });

            // User entity configuration
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                b.Property(x => x.Password).IsRequired();
                b.Property(x => x.Role).IsRequired();
                b.HasIndex(x => x.UserName).IsUnique();
            });

            // Seed data for Users
            SeedUsers(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            // Static GUIDs and DateTime to avoid dynamic values
            var adminId = new Guid("11111111-1111-1111-1111-111111111111");
            var userId = new Guid("22222222-2222-2222-2222-222222222222");
            var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Hash passwords using SHA256 - these are static values
            var adminPasswordHash = HashPassword("admin123");
            var userPasswordHash = HashPassword("user123");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    UserName = "admin@test.com",
                    Password = adminPasswordHash,
                    Role = Roles.ADMINISTRADOR,
                    EsActivo = true,
                    Creado = seedDate
                },
                new User
                {
                    Id = userId,
                    UserName = "user@test.com",
                    Password = userPasswordHash,
                    Role = Roles.USUARIO,
                    EsActivo = true,
                    Creado = seedDate
                }
            );
        }

        private byte[] HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
