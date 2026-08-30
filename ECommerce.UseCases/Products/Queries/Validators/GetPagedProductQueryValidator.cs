using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Products.Queries.Validators
{
    public class GetPagedProductQueryValidator : AbstractValidator<GetPagedProductsQuery>
    {
        public GetPagedProductQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100.");
        }
    }
}
