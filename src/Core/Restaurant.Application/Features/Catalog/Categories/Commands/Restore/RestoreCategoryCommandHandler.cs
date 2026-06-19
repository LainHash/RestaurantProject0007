using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public class RestoreCategoryCommandHandler : ICommandHandler<RestoreCategoryCommand, Result>
    {
        private readonly ICategoryService _categoryService;
        public RestoreCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<Result> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryService.RestoreCategoryAsync(request.Id, cancellationToken);
        }
    }
}
