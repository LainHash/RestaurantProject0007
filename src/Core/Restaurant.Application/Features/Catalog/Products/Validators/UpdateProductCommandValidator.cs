using FluentValidation;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;

namespace Restaurant.Application.Features.Catalog.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product Id is required.");

            RuleFor(x => x.UpdateProductRequest.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.UpdateProductRequest.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.UpdateProductRequest.CategoryId)
                .NotEmpty().WithMessage("Category Id is required.");

            RuleFor(x => x.UpdateProductRequest.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to 0.");

            RuleFor(x => x.UpdateProductRequest.Unit)
                .NotEmpty().WithMessage("Unit is required.");
            
            RuleFor(x => x.UpdateProductRequest.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0.");
        }
    }
}
