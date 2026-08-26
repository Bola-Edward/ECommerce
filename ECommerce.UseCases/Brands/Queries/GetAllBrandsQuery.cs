using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Brands.Specifications;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Brands.Queries
{
    public sealed record GetAllBrandsQuery : IQuery<Result<IReadOnlyList<GetAllBrandsResponse>>>;
}
