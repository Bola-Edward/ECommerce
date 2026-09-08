using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Errors;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Identity.Specifications;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Queries.GetUserAddress
{
    public sealed class GetUserAddressesQueryHandler(
    ICurrentUserService currentUser,
    IReadRepository<UserAddressEntity> addressRepository)
    : IRequestHandler<GetUserAddressesQuery, Result<IReadOnlyList<UserAddressResponse>>>
    {
        public async Task<Result<IReadOnlyList<UserAddressResponse>>> Handle(
            GetUserAddressesQuery request,
            CancellationToken cancellationToken)
        {
            if (currentUser.UserId is null)
                return Result<IReadOnlyList<UserAddressResponse>>.Failure(IdentityErrors.InvalidCredentials);

            var addresses = await addressRepository.ListAsync(
                new UserAddressesByUserIdSpecification(currentUser.UserId.Value),
                cancellationToken);

            return Result<IReadOnlyList<UserAddressResponse>>.Success(addresses);
        }
    }
}
