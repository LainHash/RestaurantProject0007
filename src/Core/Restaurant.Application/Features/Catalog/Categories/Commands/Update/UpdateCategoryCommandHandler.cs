using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, DataResult<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService;
        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<DataResult<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _categoryService.UpdateCategoryAsync(request.Id, request.UpdateCategoryRequest, cancellationToken);
            return response;
        }
    }
}
