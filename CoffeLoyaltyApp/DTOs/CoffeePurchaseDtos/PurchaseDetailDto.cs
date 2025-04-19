namespace CoffeLoyaltyApp.DTOs.CoffeePurchaseDtos
{
    public record PurchaseDetailDto(
       Guid PurchaseId,
       Guid CustomerId,
       Guid MenuItemId,
       DateTime PurchaseDate,
       bool IsFree
   );
}
