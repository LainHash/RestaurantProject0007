using FluentValidation;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;

namespace Restaurant.Application.Features.Catalog.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.CreateProductRequest.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.CreateProductRequest.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.CreateProductRequest.CategoryId)
                .NotEmpty().WithMessage("Category Id is required.");

            RuleFor(x => x.CreateProductRequest.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to 0.");

            RuleFor(x => x.CreateProductRequest.Unit)
                .NotEmpty().WithMessage("Unit is required.");
            
            RuleFor(x => x.CreateProductRequest.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0.");
        }
    }
}
