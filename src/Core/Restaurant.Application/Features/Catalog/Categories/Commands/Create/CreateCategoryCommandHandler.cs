using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, DataResult<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService;
        public CreateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<DataResult<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateCategorySpecification(request);
            var response = await _categoryService.CreateCategoryAsync(specification, cancellationToken);
            return response;
        }
    }
}
