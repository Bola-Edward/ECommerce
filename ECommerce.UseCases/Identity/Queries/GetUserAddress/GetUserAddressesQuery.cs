using ECommerce.Domain.Common;
using ECommerce.UseCases.Identity.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Identity.Queries.GetUserAddress
{
    public sealed record GetUserAddressesQuery
    : IQuery<Result<IReadOnlyList<UserAddressResponse>>>;
}
