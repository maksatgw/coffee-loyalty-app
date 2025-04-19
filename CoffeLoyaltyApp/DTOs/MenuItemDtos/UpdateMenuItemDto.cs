using System.ComponentModel.DataAnnotations;

namespace CoffeLoyaltyApp.DTOs.MenuItemDtos
{
    public class UpdateMenuItemDto : CreateMenuItemDto
    {
        [Required]
        public Guid MenuItemId { get; set; }
    }
}
