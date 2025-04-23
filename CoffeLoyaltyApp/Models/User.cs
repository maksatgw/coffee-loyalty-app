using CoffeeLoyaltyApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Customer"; // Admin, Barista, Customer

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Eğer bu kullanıcı bir müşteriyle bağlantılıysa
        public Guid? CustomerId { get; set; }

        // Navigation Property — 1:1 ilişki
        public Customer? Customer { get; set; }
    }
}
