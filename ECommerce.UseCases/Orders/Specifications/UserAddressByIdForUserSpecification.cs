using ECommerce.Domain.Entities;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Orders.Specifications
{

    public sealed class UserAddressByIdForUserSpecification : Specification<UserAddressEntity>
    {
        public UserAddressByIdForUserSpecification(Guid addressId, Guid userId)
        {
            Query.Where(a => a.Id == addressId && a.UserId == userId);
        }
    }
}
