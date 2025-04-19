using CoffeeLoyaltyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLoyaltyApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<CoffeePurchase> CoffeePurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CoffeePurchase>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.CoffeePurchases)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<CoffeePurchase>()
                .HasOne(p => p.MenuItem)
                .WithMany(m => m.CoffeePurchases)
                .HasForeignKey(p => p.MenuItemId);

            // Buraya bunu ekliyoruz:
            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(10, 2);
        }

    }
}
