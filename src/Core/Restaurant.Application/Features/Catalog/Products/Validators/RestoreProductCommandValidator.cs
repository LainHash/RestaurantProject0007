using FluentValidation;
using Restaurant.Application.Features.Catalog.Products.Commands.Restore;

namespace Restaurant.Application.Features.Catalog.Products.Validators
{
    public class RestoreProductCommandValidator : AbstractValidator<RestoreProductCommand>
    {
        public RestoreProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product Id is required.");
        }
    }
}
