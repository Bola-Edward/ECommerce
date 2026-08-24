using ECommerce.UseCases.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specification
{
    public interface ISpecificationBuilder<T>
    {
        ISpecificationBuilder<T> Where(Expression<Func<T, bool>> predicate);

        
        IIncludeSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> includeExpression);
        IIncludeCollectionSpecificationBuilder<T, TElement> IncludeCollection<TElement>(Expression<Func<T, IEnumerable<TElement >>> includeExpression);

        IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression);
        IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression);

        ISpecificationBuilder<T> Skip(int skip);
        ISpecificationBuilder<T> Take(int take);

        ISpecificationBuilder<T> AsNoTracking();
        ISpecificationBuilder<T> AsTracking();
    }
}


public interface IOrderSpecificationBuilder<T> : ISpecificationBuilder<T>
{
    IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> orderExpression);

    IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> orderExpression);
}


public interface IIncludeSpecificationBuilder<T, TProperty> : ISpecificationBuilder<T>
{
    IIncludeSpecificationBuilder<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, TNextProperty>> thenIncludeExpression);

}

public interface IIncludeCollectionSpecificationBuilder<T, TElement> : ISpecificationBuilder<T>
{
   IIncludeSpecificationBuilder<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TElement, TNextProperty>> thenIncludeExpression);
}   


public interface ISpecificationBuilder<T, TResult> 
{

    ISpecificationBuilder<T, TResult> Where(Expression<Func<T, bool>> predicate);


    IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression);
    IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression);

    ISpecificationBuilder<T, TResult> Skip(int skip);
    ISpecificationBuilder<T, TResult> Take(int take);

    ISpecificationBuilder<T, TResult> AsNoTracking();
    ISpecificationBuilder<T, TResult> AsTracking();

    ISpecificationBuilder<T, TResult> Select(Expression<Func<T, TResult>> selectExpression);
    ISpecificationBuilder<T, TResult> SelectMany(Expression<Func<T, IEnumerable<TResult>>> selectManyExpression);
}