using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Messaging.Apstractions
{
    public interface ISender
    {
        Task<TResponse> Send<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default);
    }
}
