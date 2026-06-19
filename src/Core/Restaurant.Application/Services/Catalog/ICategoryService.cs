using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.Application.Services.Catalog
{
    public interface ICategoryService
    {
        Task<PageResult<IEnumerable<CategoryResponse>>> 
            GetCategoriesAsync(GetAllCategoryQuery request, CancellationToken cancellationToken = default);

        Task<DataResult<CategoryResponse>>
            CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default);
    }
}
