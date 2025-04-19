namespace CoffeLoyaltyApp.DTOs.MenuItemDtos
{
    public record MenuItemDto(
        Guid MenuItemId,
        string Name,
        string? Description,
        decimal Price
    );
}
