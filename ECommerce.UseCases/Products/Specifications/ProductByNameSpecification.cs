using ECommerce.Domain.Entities;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Specifications
{
    public sealed class ProductByNameSpecification : Specification<ProductEntity>
    {
        public ProductByNameSpecification(string name) =>
            Query.Where(p => p.Name == name);
    }
}
