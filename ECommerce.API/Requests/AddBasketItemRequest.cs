using ECommerce.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Requests
{
    public sealed class AddBasketItemRequest
    {
        [Required]
        public Guid ProductId { get; init; }

        [Range(BasketItemEntity.MinQuantity, BasketItemEntity.MaxQuantity)]
        public int Quantity { get; init; } = 1;
    }
}
