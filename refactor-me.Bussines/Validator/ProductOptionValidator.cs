using FluentValidation;
using refactor_me.Domain.Entities;

namespace refactor_me.Bussines.Validator
{
    public class ProductOptionValidator: AbstractValidator<ProductOption>
    {
        public ProductOptionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required")
                .Length(1, 100).WithMessage("To loong");

            RuleFor(m => m.Description).Length(0, 500).WithMessage("To loong");
        }
    }
}
