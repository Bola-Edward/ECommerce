using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Messaging.Apstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>;
}
