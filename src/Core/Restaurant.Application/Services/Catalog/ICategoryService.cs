using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Categories.Commands.Delete;
using Restaurant.Application.Features.Catalog.Categories.Commands.Restore;
using Restaurant.Contracts.DTOs.Catalog.Categories;
namespace Restaurant.Application.Services.Catalog
{
    public interface ICategoryService
    {
        Task<PageResult<IEnumerable<CategoryResponse>>> 
            GetCategoriesAsync(GetAllCategorySpecification specification, CancellationToken cancellationToken = default);

        Task<DataResult<CategoryResponse>>
            CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default);

        Task<DataResult<CategoryResponse>>
            UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellation = default);

        Task<Result>
            DeleteCategoryAsync(DeleteCategorySpecification specification, CancellationToken cancellationToken = default);

        Task<Result>
            RestoreCategoryAsync(RestoreCategorySpecification specification, CancellationToken cancellationToken = default);
    }
}
