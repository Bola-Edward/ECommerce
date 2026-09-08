using ECommerce.Domain.Entities;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Specifications
{
    public sealed class UserAddressesByUserIdSpecification
    : Specification<UserAddressEntity, UserAddressResponse>
    {
        public UserAddressesByUserIdSpecification(Guid userId)
        {
            Query
                .Where(a => a.UserId == userId)
                .Select(a => new UserAddressResponse(
                    a.Id,
                    a.Label,
                    a.RecipientFirstName,
                    a.RecipientLastName,
                    a.PhoneNumber,
                    a.Country,
                    a.City,
                    a.Street,
                    a.PostalCode,
                    a.IsDefaultShipping,
                    a.IsDefaultBilling))
                .OrderByDescending(a => a.IsDefaultShipping)
                .ThenBy(a => a.Label);
        }
    }
}
