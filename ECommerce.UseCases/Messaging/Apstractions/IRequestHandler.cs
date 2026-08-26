using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Messaging.Apstractions
{
    public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
