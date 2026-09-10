using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Errors;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Orders.Dtos;
using ECommerce.UseCases.Orders.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler(
    ICurrentUserService currentUser,
    IReadRepository<OrderEntity> orderRepository)
    : IRequestHandler<GetOrderByIdQuery, Result<OrderResponse>>
    {
        public async Task<Result<OrderResponse>> Handle(
            GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (currentUser.UserId is null)
                return Result<OrderResponse>.Failure(OrderErrors.Unauthorized);

            var order = await orderRepository.FirstOrDefaultAsync(
                new OrderByIdForUserSpecification(request.OrderId, currentUser.UserId.Value),
                cancellationToken);

            if (order is null)
                return Result<OrderResponse>.Failure(OrderErrors.NotFound);

            return Result<OrderResponse>.Success(OrderMappings.ToResponse(order));
        }
    }
}
