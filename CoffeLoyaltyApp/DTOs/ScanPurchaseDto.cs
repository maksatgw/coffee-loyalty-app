using System.ComponentModel.DataAnnotations;

public class ScanPurchaseDto
{
    [Required] public string QrCode { get; set; }
    [Required] public Guid MenuItemId { get; set; }
}