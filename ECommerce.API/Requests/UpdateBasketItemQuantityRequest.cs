using ECommerce.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.Requests
{

    public sealed class UpdateBasketItemQuantityRequest
    {
        [Range(BasketItemEntity.MinQuantity, BasketItemEntity.MaxQuantity)]
        public int Quantity { get; init; }
    }
}
