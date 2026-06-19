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

        Task<DataResult<ProductResponse>>
            CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken);

        Task<DataResult<ProductResponse>>
            UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken);

        Task<Result>
            DeleteProductAsync(Guid id, CancellationToken cancellationToken);

        Task<Result>
            RestoreProductAsync(Guid id, CancellationToken cancellationToken);
    }
}
