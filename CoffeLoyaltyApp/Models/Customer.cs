using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeLoyaltyApp.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Required]
        public string QRCode { get; set; } = Guid.NewGuid().ToString();

        // Navigation Property
        public ICollection<CoffeePurchase>? CoffeePurchases { get; set; }
    }
}
