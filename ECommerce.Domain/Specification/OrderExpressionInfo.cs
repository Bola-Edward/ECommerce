using System.Linq.Expressions;

using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Specification
{
    // orderby (p => p.Price)
    public record OrderExpressionInfo<T>(Expression<Func<T, object?>> KeySelector, OrderType OrderType);


    public enum OrderType
    {
        OrderBy,
        OrderByDescending,
        ThenBy,
        ThenByDescending
    }

}
