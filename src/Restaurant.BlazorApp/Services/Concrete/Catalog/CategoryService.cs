using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.BlazorApp.Common.Models.Result;
using Restaurant.BlazorApp.Services.Abstraction;
using Restaurant.BlazorApp.Services.Abstraction.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.BlazorApp.Services.Concrete.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiService _apiService;

        public CategoryService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<PageResult<IEnumerable<CategoryResponse>>?> GetAllCategoriesAsync()
        {
            return await _apiService.GetAsync<PageResult<IEnumerable<CategoryResponse>>>("/api/categories?pageSize=100");
        }
    }
}
