using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Basket.Dtos
{
    public record BasketItemResponse(
    Guid ProductId,
    string ProductName,
    string PictureUrl,
    decimal UnitPrice,
    int Quantity,
    decimal LineTotal);

    public record GetBasketResponse(
        Guid BuyerId,
        IReadOnlyList<BasketItemResponse> Items,
        int TotalItems,
        decimal SubTotal)
    {
        public static GetBasketResponse From(BasketEntity basket) =>
            new(
                basket.BuyerId,
                basket.Items
                    .Select(item => new BasketItemResponse(
                        item.Id,
                        item.ProductName,
                        item.PictureUrl,
                        item.UnitPrice,
                        item.Quantity,
                        item.LineTotal))
                    .ToList(),
                basket.TotalItems,
                basket.SubTotal);
    }
}
