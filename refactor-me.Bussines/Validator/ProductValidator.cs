using FluentValidation;
using refactor_me.Domain.Entities;

namespace refactor_me.Bussines.Validator
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required")
                .Length(1, 100).WithMessage("To loong");

            RuleFor(m => m.Description).Length(0, 500).WithMessage("To loong");

            RuleFor(m => m.Price).GreaterThanOrEqualTo(0).WithMessage("Must be greater or equal to 0");

            RuleFor(m => m.DeliveryPrice).GreaterThanOrEqualTo(0).WithMessage("Must be greater or equal to 0");
        }
    }
}
