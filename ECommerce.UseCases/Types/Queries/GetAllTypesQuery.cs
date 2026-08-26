using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Types.Dtos;
using ECommerce.UseCases.Types.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Types.Queries
{
    public sealed record GetAllTypesQuery : IQuery<Result<IReadOnlyList<GetAllTypesResponse>>>;
}
