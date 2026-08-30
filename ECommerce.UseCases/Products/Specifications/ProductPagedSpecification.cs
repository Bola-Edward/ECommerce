using ECommerce.Domain.Entities;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Enums;
using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Specifications
{
    public class ProductPagedSpecification : Specification<ProductEntity, GetAllProductsResponse>
    {
        public ProductPagedSpecification(string? search = null, Guid? brandId = null, Guid? typeId = null,
            ProductSortField? sortBy = null, bool sortDescending = false, int? pageNumber = null, int? pageSize = null)
        {
            var query = Query;

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(product => product.Name.Contains(search) || product.Description.Contains(search));
            }

            if (brandId.HasValue)
            {
                query = query.Where(product => product.ProductBrandId == brandId.Value);
            }

            if (typeId.HasValue)
            {
                query = query.Where(product => product.ProductTypeId == typeId.Value);
            }

            if (sortBy is ProductSortField sortField)
            {
                ApplySort(query, sortField, sortDescending);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var skip = (pageNumber.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value).Select(p => new GetAllProductsResponse(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.PictureUrl,
                    p.ProductBrand.Name,
                    p.ProductType.Name));


            }
        }




        public void ApplySort(ISpecificationBuilder<ProductEntity, GetAllProductsResponse> query, ProductSortField sortBy, bool sortDescending)
        {
            switch (sortBy)
            {
                case ProductSortField.Name:
                    if (sortDescending)
                        query.OrderByDescending(product => product.Name);
                    else
                        query.OrderBy(product => product.Name);
                    break;

                case ProductSortField.Price:
                    if (sortDescending)
                        query.OrderByDescending(product => product.Price);
                    else
                        query.OrderBy(product => product.Price);
                    break;

                case ProductSortField.Brand:
                    if (sortDescending)
                        query.OrderByDescending(product => product.ProductBrand.Name);
                    else
                        query.OrderBy(product => product.ProductBrand.Name);
                    break;

                case ProductSortField.Type:
                    if (sortDescending)
                        query.OrderByDescending(product => product.ProductType.Name);
                    else
                        query.OrderBy(product => product.ProductType.Name);
                    break;

                default:
                    query.OrderBy(product => product.Name);
                    break;
            }
        }
    }
}
