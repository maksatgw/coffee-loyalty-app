using CoffeeLoyaltyApp.Data;
using CoffeeLoyaltyApp.Models;
using CoffeeLoyaltyApp.Services;
using CoffeLoyaltyApp.DTOs;
using CoffeLoyaltyApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CoffeLoyaltyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
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

            
            if (dto.Role.ToLower() == "customer")
            {
                customer = new Customer
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    QRCode = Guid.NewGuid().ToString() 
                };

                _context.Customers.Add(customer);
            }

        
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

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı.");

            // Şifre doğrula
            var hashedInput = HashPassword(dto.Password);
            if (hashedInput != user.PasswordHash)
                return Unauthorized("Geçersiz şifre.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Role = user.Role,
                CustomerId = user.CustomerId,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            });
        }

        [Authorize] // sadece login olanlar erişebilir
        [HttpGet("me")]
        public async Task<ActionResult<AuthResponseDto>> Me()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("Token geçersiz.");

            var userId = Guid.Parse(userIdClaim);

            var user = await _context.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            var token = _jwtService.GenerateToken(user); // token'ı yenileyebiliriz istersen

            return Ok(new AuthResponseDto
            {
                Token = token,
                Role = user.Role,
                CustomerId = user.CustomerId,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            });
        }

    }
}
