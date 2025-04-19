using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.DTOs.CustomerDtos
{
    public class CreateCustomerDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
    }
}
