using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Categories.Commands.Delete;
using Restaurant.Application.Features.Catalog.Categories.Commands.Restore;
using Restaurant.Contracts.DTOs.Catalog.Categories;
using Restaurant.Application.Features.Catalog.Categories.Commands.Create;
using Restaurant.Application.Features.Catalog.Categories.Commands.Update;
namespace Restaurant.Application.Services.Catalog
{
    public interface ICategoryService
    {
        Task<PageResult<IEnumerable<CategoryResponse>>> 
            GetCategoriesAsync(GetAllCategorySpecification specification, CancellationToken cancellationToken = default);

        Task<DataResult<CategoryResponse>>
            CreateCategoryAsync(CreateCategorySpecification specification, CancellationToken cancellationToken = default);

        Task<DataResult<CategoryResponse>>
            UpdateCategoryAsync(UpdateCategorySpecification specification, CancellationToken cancellation = default);

        Task<Result>
            DeleteCategoryAsync(DeleteCategorySpecification specification, CancellationToken cancellationToken = default);

        Task<Result>
            RestoreCategoryAsync(RestoreCategorySpecification specification, CancellationToken cancellationToken = default);
    }
}
