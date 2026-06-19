using FluentValidation;
using Restaurant.Application.Features.Catalog.Categories.Commands.Update;

namespace Restaurant.Application.Features.Catalog.Categories.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category Id is required.");

            RuleFor(x => x.UpdateCategoryRequest.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.UpdateCategoryRequest.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
