using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Types.Dtos;
using ECommerce.UseCases.Types.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Types.Queries
{
    public class GetAllTypesQuery
    {
        private readonly IRepository<ProductTypeEntity> _productTypeRepository;

        public GetAllTypesQuery(IRepository<ProductTypeEntity> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }


        public async Task<Result<IReadOnlyList<GetAllTypesResponse>>> ExecuteAsync(CancellationToken ct = default)
        {
            var types = await _productTypeRepository.ListAsync(new ProductTypeListSpecification(), ct);
            return Result<IReadOnlyList<GetAllTypesResponse>>.Success(types);
        }

    }
}
