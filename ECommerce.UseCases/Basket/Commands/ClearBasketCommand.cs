using ECommerce.Domain.Common;
using ECommerce.Domain.IRepositories;
using ECommerce.UseCases.Basket.Dtos;
using ECommerce.UseCases.Messaging.Apstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Basket.Commands
{
    public sealed record ClearBasketCommand(Guid BuyerId) : ICommand<Result<GetBasketResponse>>;

    public sealed class ClearBasketCommandHandler(IBasketStore basketStore)
        : IRequestHandler<ClearBasketCommand, Result<GetBasketResponse>>
    {
        public async Task<Result<GetBasketResponse>> Handle(
            ClearBasketCommand request,
            CancellationToken cancellationToken)
        {
            var basket = await basketStore.GetOrCreateAsync(request.BuyerId, cancellationToken);
            basket.Clear();
            await basketStore.SaveAsync(basket, cancellationToken);
            return Result<GetBasketResponse>.Success(GetBasketResponse.From(basket));
        }
    }
}
