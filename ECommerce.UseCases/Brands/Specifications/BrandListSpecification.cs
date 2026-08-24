using ECommerce.Domain.Entities;
using ECommerce.UseCases.Brands.Dtos;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Brands.Specifications
{
    public sealed class BrandListSpecification : Specification<ProductBrandEntity, GetAllBrandsResponse>
    {
        public BrandListSpecification()
        {
            Query
                .Select(brand => new GetAllBrandsResponse(brand.Id, brand.Name))
                .OrderBy(brand => brand.Name);

        }
    }
}
