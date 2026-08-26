using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Messaging.Implementation
{
    public interface IRequestHandlerExecutor
    {
        Type RequestType { get; }
        Task<object?> ExecuteAsync(object request, CancellationToken cancellationToken);
    }
}
