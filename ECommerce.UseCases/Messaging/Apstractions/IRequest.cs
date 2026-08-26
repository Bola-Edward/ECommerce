using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Messaging.Apstractions
{
    /// <summary>
    /// Marker for a request that returns <typeparamref name="TResponse"/>.
    /// Implemented by commands and queries; dispatched via <see cref="ISender"/>.
    /// </summary>
    public interface IRequest<out TResponse>;
}
