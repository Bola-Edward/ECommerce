using ECommerce.Domain.Entities;
using ECommerce.UseCases.Specification;
using ECommerce.UseCases.Types.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Types.Specifications
{
    public sealed class ProductTypeListSpecification : Specification<ProductTypeEntity, GetAllTypesResponse>
    {
        public ProductTypeListSpecification()
        {
            Query
                .Select(type => new GetAllTypesResponse(type.Id, type.Name))
                .OrderBy(type => type.Name);
        }
    }
}
