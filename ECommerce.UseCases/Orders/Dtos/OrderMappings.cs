using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Dtos
{

    public static class OrderMappings
    {
        public static OrderResponse ToResponse(OrderEntity order) =>
            new(
                order.Id,
                order.UserId,
                order.Status.ToString(),
                order.DeliveryMethodId,
                order.DeliveryMethodName,
                order.DeliveryMethodPrice,
                order.DeliveryMethodEstimatedTime,
                new OrderShippingAddressResponse(
                    order.ShippingRecipientFirstName,
                    order.ShippingRecipientLastName,
                    order.ShippingPhoneNumber,
                    order.ShippingCountry,
                    order.ShippingCity,
                    order.ShippingStreet,
                    order.ShippingPostalCode),
                order.SubTotal,
                order.ShippingCost,
                order.Total,
                order.CreatedAt,
                order.Items.Select(i => new OrderItemResponse(
                    i.Id,
                    new ProductItemOrderedResponse(
                        i.ItemOrdered.ProductId,
                        i.ItemOrdered.ProductName,
                        i.ItemOrdered.PictureUrl,
                        i.ItemOrdered.UnitPrice),
                    i.Quantity,
                    i.LineTotal)).ToList());
    }
}
