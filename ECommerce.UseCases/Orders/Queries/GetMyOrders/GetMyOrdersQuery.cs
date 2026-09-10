using ECommerce.Domain.Common;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Queries.GetMyOrders
{
    public sealed record GetMyOrdersQuery(
    int PageNumber = 1,
    int PageSize = 10) : IQuery<Result<PagedResult<OrderResponse>>>;
}
