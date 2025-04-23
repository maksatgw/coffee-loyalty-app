using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [Required]
        public string Role { get; set; } = "Customer";
    }
}
