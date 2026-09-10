using ECommerce.Domain.Entities;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Specifications
{
    public sealed class OrderByIdForUserSpecification : Specification<OrderEntity>
    {
        public OrderByIdForUserSpecification(Guid orderId, Guid userId, bool tracking = false)
        {
            var query = Query
                .Where(o => o.Id == orderId && o.UserId == userId)
                .Include(o => o.Items);

            if (tracking)
                query.AsTracking();
            else
                query.AsNoTracking();
        }
    }

    public sealed class OrdersByUserIdPagedSpecification : Specification<OrderEntity>
    {
        public OrdersByUserIdPagedSpecification(Guid userId, int pageNumber, int pageSize)
        {
            Query
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();
        }
    }

    public sealed class OrdersByUserIdCountSpecification : Specification<OrderEntity>
    {
        public OrdersByUserIdCountSpecification(Guid userId)
        {
            Query.Where(o => o.UserId == userId).AsNoTracking();
        }
    }

    public sealed class OrderByPaymentIntentSpecification : Specification<OrderEntity>
    {
        public OrderByPaymentIntentSpecification(string paymentIntentId, bool tracking = false)
        {
            var query = Query.Where(o => o.PaymentIntentId == paymentIntentId);

            if (tracking)
                query.AsTracking();
            else
                query.AsNoTracking();
        }
    }
}
