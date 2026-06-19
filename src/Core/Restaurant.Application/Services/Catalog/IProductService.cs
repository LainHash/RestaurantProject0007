using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Services.Catalog
{
    public interface IProductService
    {
        Task<PageResult<IEnumerable<ProductResponse>>>
            GetProductsAsync(GetAllProductQuery request, CancellationToken cancellationToken);

        Task<DataResult<ProductResponse>>
            GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
