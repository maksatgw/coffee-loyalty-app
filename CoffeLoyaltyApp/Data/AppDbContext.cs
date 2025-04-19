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

            // Opsiyonel: İlişki ayarları (EF genelde otomatik tanır ama senin için örnek):
            modelBuilder.Entity<CoffeePurchase>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.CoffeePurchases)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<CoffeePurchase>()
                .HasOne(p => p.MenuItem)
                .WithMany(m => m.CoffeePurchases)
                .HasForeignKey(p => p.MenuItemId);
        }
    }
}
