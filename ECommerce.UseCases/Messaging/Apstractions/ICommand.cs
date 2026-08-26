using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Messaging.Apstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>;
}
