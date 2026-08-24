using ECommerce.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.UseCases.Specification
{
    public abstract class Specification<T> : ISpecification<T>
    {
        private readonly List<Expression<Func<T, bool>>> _whereExpressions = new List<Expression<Func<T, bool>>>();
        private readonly List<Expression<Func<T, object>>> _includeExpressions = new List<Expression<Func<T, object>>>();
        private readonly List<OrderExpressionInfo<T>> _orderExpressions = new List<OrderExpressionInfo<T>>();
        private readonly List<IncludeExpressionInfo> _thenIncludeExpressions = new List<IncludeExpressionInfo>();

        // builder
        protected ISpecificationBuilder<T> Query => new SpecificationBuilder<T>(this);

        public IReadOnlyList<Expression<Func<T, bool>>> WhereExpressions => _whereExpressions;
        public IReadOnlyList<Expression<Func<T, object>>> IncludeExpressions => _includeExpressions;
        public IReadOnlyList<OrderExpressionInfo<T>> OrderExpressions => _orderExpressions;
        public IReadOnlyList<IncludeExpressionInfo> ThenIncludeExpressions => _thenIncludeExpressions;

        // Builder methods to add expressions to the specification
        public void AddWhere(Expression<Func<T, bool>> whereExpression)
        {
            _whereExpressions.Add(whereExpression);
        }

        public Expression<Func<T, object>> AddInclude<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            // Expression<Func<T, TProperty>> TO Expression<Func<T, object>>

            Expression<Func<T, object>> lambda = Expression.Lambda<Func<T, object>>(
                includeExpression.Body,
                includeExpression.Parameters
            );

            _includeExpressions.Add(lambda);

            return lambda;
        }

        public void AddOrder(OrderExpressionInfo<T> orderExpressionInfo)
        {
            _orderExpressions.Add(orderExpressionInfo);
        }

        public void AddThenInclude(LambdaExpression previousExpression, LambdaExpression thenIncludeExpression)
        {
            _thenIncludeExpressions.Add(new IncludeExpressionInfo(thenIncludeExpression, previousExpression));
        }


        public void SetSkip(int skip)
        {
            Skip = skip;
        }

        public void SetTake(int take)
        {
            Take = take;
        }

        public void SetNoTracking()
        {
            IsTrackingEnabled = false;
        }

        public void SetTracking()
        {
            IsTrackingEnabled = true;
        }

        public int? Skip { get; private set; }
        public int? Take { get; private set; }
        public bool IsPaginEnabled => Skip.HasValue || Take.HasValue;


        public bool IsTrackingEnabled { get; private set; }


    }



    public abstract class Specification<T, TResult> : Specification<T>, ISpecification<T, TResult>
    {
        public Expression<Func<T, TResult>>? SelectExpression { get; private set; }
        public Expression<Func<T, IEnumerable<TResult>>>? SelectManyExpression { get; private set; }


        // builder 
        protected new ISpecificationBuilder<T, TResult> Query => new SpecificationBuilder<T, TResult>(this);


        public void SetSelect(Expression<Func<T, TResult>> selectExpression)
        {
            SelectExpression = selectExpression;
        }

        public void SetSelectMany(Expression<Func<T, IEnumerable<TResult>>> selectManyExpression)
        {
            SelectManyExpression = selectManyExpression;
        }
    }
}
