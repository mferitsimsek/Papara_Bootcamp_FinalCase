using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;
using Papara.CaptainStore.Domain.Entities.LocationEntities;
using Papara.CaptainStore.Domain.Entities.OrderEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;
using Papara.CaptainStore.Persistence.Configurations.CategoryConfigurations;
using Papara.CaptainStore.Persistence.Configurations.CouponConfigurations;
using Papara.CaptainStore.Persistence.Configurations.CustomerAccountConfigurations;
using Papara.CaptainStore.Persistence.Configurations.LocationConfigurations;
using Papara.CaptainStore.Persistence.Configurations.ProductConfigurations;

namespace Papara.CaptainStore.Persistence.Contexts
{
    public class MSSqlContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public MSSqlContext(DbContextOptions<MSSqlContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                        .HasKey(login => new { login.LoginProvider, login.ProviderKey });

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerAccountConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.Entity<Product>()
                .HasMany(product => product.Categories)
                .WithMany(category => category.Products)
                .UsingEntity<Dictionary<string, object>>(
                "ProductCategory",
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Restrict)
            );

            //modelBuilder.Entity<Product>()
            //.HasMany(p => p.Categories)
            //.WithMany(c => c.Products)
            //.UsingEntity<Dictionary<string, object>>(
            //    "CategoryProduct", 
            //    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.Restrict),
            //    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId").OnDelete(DeleteBehavior.Restrict),
            //    j =>
            //    {
            //        j.HasKey("CategoryId", "ProductId"); // Birincil anahtar konfigürasyonu
            //    });
            modelBuilder.Entity<AppUser>()
            .HasOne(u => u.CustomerAccount)
            .WithOne(w => w.AppUser)
            .HasForeignKey<CustomerAccount>(w => w.AppUserId);
        }
    }
}
