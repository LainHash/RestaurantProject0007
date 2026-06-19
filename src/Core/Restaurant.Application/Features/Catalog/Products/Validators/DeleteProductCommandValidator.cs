using FluentValidation;
using Restaurant.Application.Features.Catalog.Products.Commands.Delete;

namespace Restaurant.Application.Features.Catalog.Products.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product Id is required.");
        }
    }
}
