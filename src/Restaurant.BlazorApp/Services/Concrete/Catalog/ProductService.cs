using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.BlazorApp.Common.Models.Result;
using Restaurant.BlazorApp.Services.Abstraction;
using Restaurant.BlazorApp.Services.Abstraction.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.BlazorApp.Services.Concrete.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IApiService _apiService;

        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<PageResult<IEnumerable<ProductResponse>>?> GetAllProductsAsync(string? categoryName = null)
        {
            var endpoint = "/api/products?pageSize=100";
            if (!string.IsNullOrEmpty(categoryName))
            {
                endpoint += $"&categoryName={System.Uri.EscapeDataString(categoryName)}";
            }
            return await _apiService.GetAsync<PageResult<IEnumerable<ProductResponse>>>(endpoint);
        }
    }
}
