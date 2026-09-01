using ECommerce.Domain.Common;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Basket.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Basket.Commands
{
    public sealed record RemoveBasketItemCommand(Guid BuyerId, Guid ProductId)
     : ICommand<Result<GetBasketResponse>>;

    public sealed class RemoveBasketItemCommandHandler(IBasketStore basketStore)
        : IRequestHandler<RemoveBasketItemCommand, Result<GetBasketResponse>>
    {
        public async Task<Result<GetBasketResponse>> Handle(
            RemoveBasketItemCommand request,
            CancellationToken cancellationToken)
        {
            var basket = await basketStore.GetOrCreateAsync(request.BuyerId, cancellationToken);

            var removeResult = basket.RemoveItem(request.ProductId);
            if (removeResult.IsFailure)
                return Result<GetBasketResponse>.Failure(removeResult.Error);

            await basketStore.SaveAsync(basket, cancellationToken);
            return Result<GetBasketResponse>.Success(GetBasketResponse.From(basket));
        }
    }
}
