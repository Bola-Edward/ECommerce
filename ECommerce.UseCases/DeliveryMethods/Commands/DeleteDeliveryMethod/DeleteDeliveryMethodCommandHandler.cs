using ECommerce.Domain;
using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Errors;
using ECommerce.Domain.Repositories;
using ECommerce.UseCases.DeliveryMethods.Specifications;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.DeliveryMethods.Commands.DeleteDeliveryMethod
{
    public sealed class DeleteDeliveryMethodCommandHandler(
    IRepository<DeliveryMethodEntity> repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDeliveryMethodCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteDeliveryMethodCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await repository.FirstOrDefaultAsync(new DeliveryMethodByIdSpecification(request.Id), cancellationToken);

            if (entity is null)
                return Result.Failure(DeliveryMethodErrors.NotFound);

            repository.Delete(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
