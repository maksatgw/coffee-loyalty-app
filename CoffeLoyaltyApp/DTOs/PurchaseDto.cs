using System.ComponentModel.DataAnnotations;

namespace CoffeeLoyaltyApp.DTOs
{
    public class PurchaseDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid MenuItemId { get; set; }
    }
}
