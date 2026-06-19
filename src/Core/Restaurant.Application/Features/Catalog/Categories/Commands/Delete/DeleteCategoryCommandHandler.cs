using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, Result>
    {
        private readonly ICategoryService _categoryService;
        public DeleteCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteCategoryAsync(request.Id, cancellationToken);
        }
    }
}
