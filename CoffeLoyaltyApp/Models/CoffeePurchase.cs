using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeLoyaltyApp.Models
{
    public class CoffeePurchase
    {
        [Key]
        public Guid PurchaseId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid MenuItemId { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public bool IsFree { get; set; } = false;

        // Navigation Properties
        [JsonIgnore]
        public Customer? Customer { get; set; }
        [JsonIgnore]
        public MenuItem? MenuItem { get; set; }
    }
}
