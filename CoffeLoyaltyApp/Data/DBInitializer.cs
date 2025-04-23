using CoffeeLoyaltyApp.Models;
using CoffeLoyaltyApp.Models;
using System.Security.Cryptography;
using System.Text;

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

            if (!context.Users.Any())
            {
                // SHA256 ile örnek hashleme
                string Hash(string pwd)
                {
                    using var sha = SHA256.Create();
                    var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                    return Convert.ToBase64String(bytes);
                }

                // Admin & Barista
                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = Hash("admin123"),
                    Role = "Admin"
                };

                var barista = new User
                {
                    Username = "barista",
                    PasswordHash = Hash("barista123"),
                    Role = "Barista"
                };

                // Customer + QR + User
                var customer = new Customer
                {
                    Name = "Ahmet Yılmaz",
                    Email = "ahmet@example.com",
                    Phone = "05001234567",
                    QRCode = Guid.NewGuid().ToString()
                };

                var user = new User
                {
                    Username = "ahmet",
                    PasswordHash = Hash("123456"),
                    Role = "Customer",
                    Customer = customer
                };

                context.Users.AddRange(admin, barista, user);
            }


            context.SaveChanges();
        }
    }
}
