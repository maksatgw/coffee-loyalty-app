using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.DTOs.CustomerDtos
{
    public class UpdateCustomerDto : CreateCustomerDto
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
