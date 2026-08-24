using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.Domain.Specification
{
    public interface ISpecification<T>
    {
        // WHERE(product => product.Price > 100)
        IReadOnlyList<Expression<Func<T, bool>>> WhereExpressions { get; }

        // INCLUDE(p => p.ProductBrand)
        IReadOnlyList<Expression<Func<T, object>>> IncludeExpressions { get; }

        // ORDERBY(p => p.Price)
        IReadOnlyList<OrderExpressionInfo<T>> OrderExpressions { get; }

        // THENINCLUDE
        IReadOnlyList<IncludeExpressionInfo> ThenIncludeExpressions { get; }


        // SKIP(10)
        int? Skip { get; }

        // TAKE(10)
        int? Take { get; }

        bool IsPaginEnabled { get; }

        bool IsTrackingEnabled { get; }
    }


    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        // SELECT(p => new ProductDto { Id = p.Id, Name = p.Name })
        Expression<Func<T, TResult>>? SelectExpression { get; }

        // SELECTMANY(p => p.ProductTags.Select(pt => pt.Tag))
        Expression<Func<T, IEnumerable<TResult>>>? SelectManyExpression { get; }
    }

}