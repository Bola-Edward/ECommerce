using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Messaging.Apstractions;
using ECommerce.UseCases.Products.Dtos;
using ECommerce.UseCases.Products.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries.Handlers
{
    public sealed class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, Result<PagedResult<GetAllProductsResponse>>>
    {

        private readonly IReadRepository<ProductEntity> _repository;

        public GetPagedProductsQueryHandler(IReadRepository<ProductEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedResult<GetAllProductsResponse>>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var countSpec = new ProductPagedSpecification(request.Search, request.BrandId, request.TypeId);
            var listSpecification = new ProductPagedSpecification(request.Search, request.BrandId, request.TypeId, request.SortBy, request.SortDescending, request.PageNumber, request.PageSize);

            var totalCount = await _repository.CountAsync(countSpec, cancellationToken);

            var items = await _repository.ListAsync(listSpecification, cancellationToken);

            return Result<PagedResult<GetAllProductsResponse>>.Success(new PagedResult<GetAllProductsResponse>(items, totalCount));
        }
    }
}
