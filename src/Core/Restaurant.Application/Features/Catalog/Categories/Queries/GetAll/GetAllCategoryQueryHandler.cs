using Restaurant.Application.Common.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;

public class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryQuery, DataResult<IEnumerable<CategoryResponse>>>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoryQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<DataResult<IEnumerable<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var response = await _categoryService.GetCategoriesAsync(request, cancellationToken);

        return response;
    }
}
