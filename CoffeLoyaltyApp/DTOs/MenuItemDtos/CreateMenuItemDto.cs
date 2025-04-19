using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.DTOs.MenuItemDtos
{
    public class CreateMenuItemDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required, Range(0, 999)]
        public decimal Price { get; set; }
    }
}
