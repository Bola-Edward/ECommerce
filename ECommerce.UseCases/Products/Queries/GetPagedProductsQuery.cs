using ECommerce.Domain.Common;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries
{
    public sealed record GetPagedProductsQuery(
    int PageNumber = 1,
    int PageSize = 5,
    string? Search = null,
    Guid? BrandId = null,
    Guid? TypeId = null,
    ProductSortField? SortBy = ProductSortField.Name,
    bool SortDescending = false) : IQuery<Result<PagedResult<GetAllProductsResponse>>>;

}
