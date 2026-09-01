using ECommerce.Domain.Common;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Basket.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Basket.Commands
{
    public sealed record UpdateBasketItemQuantityCommand(Guid BuyerId, Guid ProductId, int Quantity)
    : ICommand<Result<GetBasketResponse>>;

    public sealed class UpdateBasketItemQuantityCommandHandler(IBasketStore basketStore)
        : IRequestHandler<UpdateBasketItemQuantityCommand, Result<GetBasketResponse>>
    {
        public async Task<Result<GetBasketResponse>> Handle(
            UpdateBasketItemQuantityCommand request,
            CancellationToken cancellationToken)
        {
            var basket = await basketStore.GetOrCreateAsync(request.BuyerId, cancellationToken);

            var updateResult = basket.UpdateItemQuantity(request.ProductId, request.Quantity);
            if (updateResult.IsFailure)
                return Result<GetBasketResponse>.Failure(updateResult.Error);

            await basketStore.SaveAsync(basket, cancellationToken);
            return Result<GetBasketResponse>.Success(GetBasketResponse.From(basket));
        }
    }
}
