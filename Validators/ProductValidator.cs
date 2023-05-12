namespace Ndumiso_Assessment_2023_05_12.Validators
{
    using FluentValidation;

    using Ndumiso_Assessment_2023_05_12.Models;

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            this.RuleSet("Edit", () =>
            {
                this.GeneralRules();
                this.RuleFor(_ => _.Id)
                    .GreaterThan(0);
            });

            this.RuleSet("Create", () =>
            {
                this.GeneralRules();
            });

            this.RuleSet("Delete", () =>
            {
                this.RuleFor(_ => _.Id)
                    .GreaterThan(0)
                    .WithMessage("Product does not exist.");
            });

            this.GeneralRules();
        }
        private void GeneralRules()
        {
            this.RuleFor(_ => _.Name)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Name must be less than 50 characters");

            this.RuleFor(_ => _.Description)
                .NotEmpty();

            this.RuleFor(_ => _.Price)
                .NotEmpty().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .PrecisionScale(10,2,true).WithMessage("Price can not have more than 2 decimal places and 10 digits");

            this.RuleFor(_ => _.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
