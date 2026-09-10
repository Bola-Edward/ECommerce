namespace ECommerce.API.Models
{
    public sealed record UpdateDeliveryMethodRequest(
    string Name,
    decimal Price,
    string EstimatedDeliveryTime,
    string? Description = null,
    bool IsAvailable = true,
    int DisplayOrder = 0);
}
