using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.BlazorApp.Common.Models.Result;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.BlazorApp.Services.Abstraction.Catalog
{
    public interface IProductService
    {
        Task<PageResult<IEnumerable<ProductResponse>>?> GetAllProductsAsync(string? categoryName = null);
    }
}
