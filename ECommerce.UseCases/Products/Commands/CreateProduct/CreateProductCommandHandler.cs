using ECommerce.Domain;
using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.Common.Interfaces;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        private readonly IRepository<ProductEntity> _productRepository;

        public CreateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IPhotoService photoService,
            IRepository<ProductEntity> productRepository)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _productRepository = productRepository;
        }

        public async Task<Result<Guid>> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var pictureUrl = await _photoService
                .UploadPhotoAsync(request.Image);

            var id = Guid.NewGuid();

            var productResult = ProductEntity.Create(
                id,
                request.Name,
                request.Description,
                pictureUrl,
                request.Price,
                request.ProductBrandId,
                request.ProductTypeId);

            if (productResult.IsFailure)
                return Result<Guid>.Failure(productResult.Error);

            _productRepository.Add(
                 productResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(id);
        }
    }
}