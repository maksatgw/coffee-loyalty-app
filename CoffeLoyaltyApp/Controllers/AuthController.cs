using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using CoffeLoyaltyApp.DTOs;
using CoffeLoyaltyApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CoffeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // POST: /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Username alınmış mı?
            var existing = await _context.Users.AnyAsync(u => u.Username == dto.Username);
            if (existing)
                return BadRequest("Username is already taken.");

            Customer? customer = null;

            // Eğer müşteri ise, Customer kaydı da oluştur
            if (dto.Role.ToLower() == "customer")
            {
                customer = new Customer
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    QRCode = Guid.NewGuid().ToString() // QR kod verisi, görsel değil!
                };

                _context.Customers.Add(customer);
            }

            // Şifreyi hashle
            string passwordHash = HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                Role = dto.Role,
                Customer = customer // Müşteri ile bağlanıyor
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }

        // 🚧 Simple hash method (gerçek projede bcrypt kullan!)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
