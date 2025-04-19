using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeLoyaltyApp.Models
{
    public class MenuItem
    {
        [Key]
        public Guid MenuItemId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 999)]
        public decimal Price { get; set; }

        // Navigation
        public ICollection<CoffeePurchase>? CoffeePurchases { get; set; }
    }
}
