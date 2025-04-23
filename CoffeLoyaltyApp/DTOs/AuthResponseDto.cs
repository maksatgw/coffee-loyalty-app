namespace CoffeLoyaltyApp.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public Guid? CustomerId { get; set; } // müşteri login olduysa

        public DateTime Expiration { get; set; }
    }
}
