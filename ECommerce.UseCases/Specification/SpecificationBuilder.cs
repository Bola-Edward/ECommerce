using ECommerce.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specification
{
    public class SpecificationBuilder<T> : ISpecificationBuilder<T>
    {
        protected readonly Specification<T> _specification;

        public SpecificationBuilder(Specification<T> specification)
        {
            _specification = specification;
        }

        public ISpecificationBuilder<T> AsNoTracking()
        {
            _specification.SetNoTracking();
            return this;
        }

        public ISpecificationBuilder<T> AsTracking()
        {
            _specification.SetTracking();
            return this;
        }

        public IIncludeSpecificationBuilder<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            var parentSpecification = _specification.AddInclude(includeExpression);
            return new IncludeSpecificationBuilder<T, TProperty>(_specification, parentSpecification);
        }

        public IIncludeCollectionSpecificationBuilder<T, TElement> IncludeCollection<TElement>(Expression<Func<T, IEnumerable<TElement>>> includeExpression)
        {
            var parentSpecification = _specification.AddInclude(includeExpression);
            return new IncludeCollectionSpecificationBuilder<T, TElement>(_specification, parentSpecification);
        }

        public IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression)
        {
            _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.OrderBy));
            return new OrderSpecificationBuilder<T>(_specification);
        }

        public IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression)
        {
            _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.OrderByDescending));
            return new OrderSpecificationBuilder<T>(_specification);
        }

        public ISpecificationBuilder<T> Skip(int skip)
        {
            _specification.SetSkip(skip);
            return this;
        }

        public ISpecificationBuilder<T> Take(int take)
        {
            _specification.SetTake(take);
            return this;
        }

        public ISpecificationBuilder<T> Where(Expression<Func<T, bool>> predicate)
        {
            _specification.AddWhere(predicate);
            return this;
        }
    }



    public sealed class SpecificationBuilder<T, TResult> : ISpecificationBuilder<T, TResult>
    {

        private readonly Specification<T, TResult> _specification;
        private readonly SpecificationBuilder<T> _builder;

        public SpecificationBuilder(Specification<T, TResult> specification)
        {
            _specification = specification;
            _builder = new SpecificationBuilder<T>(specification);
        }

        public ISpecificationBuilder<T, TResult> AsNoTracking()
        {
            _builder.AsNoTracking();
            return this;
        }

        public ISpecificationBuilder<T, TResult> AsTracking()
        {
            _builder.AsTracking();
            return this;
        }


        public IOrderSpecificationBuilder<T> OrderBy(Expression<Func<T, object?>> orderExpression)
        {
            _builder.OrderBy(orderExpression);
            return new OrderSpecificationBuilder<T>(_specification);
        }

        public IOrderSpecificationBuilder<T> OrderByDescending(Expression<Func<T, object?>> orderExpression)
        {
            _builder.OrderByDescending(orderExpression);
            return new OrderSpecificationBuilder<T>(_specification);
        }

        public ISpecificationBuilder<T, TResult> Select(Expression<Func<T, TResult>> selectExpression)
        {
            _specification.SetSelect(selectExpression);
            return this;
        }

        public ISpecificationBuilder<T, TResult> SelectMany(Expression<Func<T, IEnumerable<TResult>>> selectManyExpression)
        {
            _specification.SetSelectMany(selectManyExpression);
            return this;
        }

        public ISpecificationBuilder<T, TResult> Skip(int skip)
        {
            _builder.Skip(skip);
            return this;
        }

        public ISpecificationBuilder<T, TResult> Take(int take)
        {
            _builder.Take(take);
            return this;
        }

        public ISpecificationBuilder<T, TResult> Where(Expression<Func<T, bool>> predicate)
        {
            _builder.Where(predicate);
            return this;
        }
    }

    public sealed class IncludeSpecificationBuilder<T, TProperty> : SpecificationBuilder<T>, IIncludeSpecificationBuilder<T, TProperty>
    {
        private readonly LambdaExpression _parent;

        public IncludeSpecificationBuilder(Specification<T> specification, LambdaExpression parent) : base(specification)
        {
            _parent = parent;
        }

        public IIncludeSpecificationBuilder<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, TNextProperty>> thenIncludeExpression)
        {
            _specification.AddThenInclude(_parent, thenIncludeExpression);
            return new IncludeSpecificationBuilder<T, TNextProperty>(_specification, thenIncludeExpression);
        }
    }



    public sealed class IncludeCollectionSpecificationBuilder<T, TElement> : SpecificationBuilder<T>, IIncludeCollectionSpecificationBuilder<T, TElement>
    {
        private readonly LambdaExpression _parent;

        public IncludeCollectionSpecificationBuilder(Specification<T> specification, LambdaExpression parent) : base(specification)
        {
            _parent = parent;
        }

        public IIncludeSpecificationBuilder<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TElement, TNextProperty>> thenIncludeExpression)
        {
            _specification.AddThenInclude(_parent, thenIncludeExpression);
            return new IncludeSpecificationBuilder<T, TNextProperty>(_specification, thenIncludeExpression);
        }
    }



    public sealed class OrderSpecificationBuilder<T> : SpecificationBuilder<T>, IOrderSpecificationBuilder<T>
    {
        public OrderSpecificationBuilder(Specification<T> specification) : base(specification)
        {
        }
        public IOrderSpecificationBuilder<T> ThenBy(Expression<Func<T, object?>> orderExpression)
        {
            _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.ThenBy));
            return this;
        }
        public IOrderSpecificationBuilder<T> ThenByDescending(Expression<Func<T, object?>> orderExpression)
        {
            _specification.AddOrder(new OrderExpressionInfo<T>(orderExpression, OrderType.ThenByDescending));
            return this;
        }
    }

}


