using CoffeeLoyaltyApp.Models;

namespace CoffeeLoyaltyApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.MenuItems.Any())
            {
                context.MenuItems.AddRange(
                    new MenuItem { Name = "Espresso", Description = "Kısa, sert kahve", Price = 45 },
                    new MenuItem { Name = "Latte", Description = "Sütlü yumuşak kahve", Price = 55 },
                    new MenuItem { Name = "Cappuccino", Description = "Bol köpüklü", Price = 50 }
                );
            }

            if (!context.Customers.Any())
            {
                var c1 = new Customer
                {
                    Name = "Ahmet Yılmaz",
                    Email = "ahmet@example.com",
                    Phone = "05551112233"
                };

                var c2 = new Customer
                {
                    Name = "Zeynep Demir",
                    Email = "zeynep@example.com",
                    Phone = "05443332211"
                };

                context.Customers.AddRange(c1, c2);
            }

            context.SaveChanges();
        }
    }
}
