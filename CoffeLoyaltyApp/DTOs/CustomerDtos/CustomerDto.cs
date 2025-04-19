namespace CoffeLoyaltyApp.DTOs.CustomerDtos
{
    public record CustomerDto(
        Guid CustomerId,
        string Name,
        string? Email,
        string? Phone,
        string QRCode
    );
}
