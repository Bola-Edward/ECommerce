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

namespace ECommerce.UseCases.Orders.Queries.GetMyOrders
{
    public sealed class GetMyOrdersQueryHandler(
    ICurrentUserService currentUser,
    IReadRepository<OrderEntity> orderRepository)
    : IRequestHandler<GetMyOrdersQuery, Result<PagedResult<OrderResponse>>>
    {
        public async Task<Result<PagedResult<OrderResponse>>> Handle(
            GetMyOrdersQuery request,
            CancellationToken cancellationToken)
        {
            if (currentUser.UserId is null)
                return Result<PagedResult<OrderResponse>>.Failure(OrderErrors.Unauthorized);

            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize is < 1 or > 50 ? 10 : request.PageSize;
            var userId = currentUser.UserId.Value;

            var totalCount = await orderRepository.CountAsync(
                new OrdersByUserIdCountSpecification(userId),
                cancellationToken);

            var orders = await orderRepository.ListAsync(
                new OrdersByUserIdPagedSpecification(userId, pageNumber, pageSize),
                cancellationToken);

            var items = orders.Select(OrderMappings.ToResponse).ToList();

            return Result<PagedResult<OrderResponse>>.Success(
                new PagedResult<OrderResponse>(items, totalCount));
        }
    }
}
