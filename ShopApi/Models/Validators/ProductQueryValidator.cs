using FluentValidation;
using ShopApi.Entities;
using System;
using System.Linq;

namespace ShopApi.Models.Validators
{
    public class ProductQueryValidator : AbstractValidator<PaginationQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
     
        private string[] allowedSortByColumnNames =
            {nameof(Product.Name), nameof(Product.Category), nameof(Product.Description)};
        public ProductQueryValidator()
        {
         

            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");



    }


    }
}
