using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.BlazorApp.Common.Models.Result;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.BlazorApp.Services.Abstraction.Catalog
{
    public interface ICategoryService
    {
        Task<PageResult<IEnumerable<CategoryResponse>>?> GetAllCategoriesAsync();
    }
}
