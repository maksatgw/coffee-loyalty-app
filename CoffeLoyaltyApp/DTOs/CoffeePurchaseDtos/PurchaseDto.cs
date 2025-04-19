using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.DTOs.CoffeePurchaseDtos
{
    public class PurchaseDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid MenuItemId { get; set; }
    }
}
