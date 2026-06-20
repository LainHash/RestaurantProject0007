using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Delete;
using Restaurant.Application.Features.Catalog.Products.Commands.Restore;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Products.Queries.GetOne;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Services.Catalog
{
    public interface IProductService
    {
        Task<PageResult<IEnumerable<ProductResponse>>>
            GetProductsAsync(GetAllProductSpecification specification, CancellationToken cancellationToken);

        Task<DataResult<ProductResponse>>
            GetProductByIdAsync(GetProductByIdSpecification specification, CancellationToken cancellationToken);

        Task<DataResult<ProductResponse>>
            CreateProductAsync(CreateProductSpecification specification, CancellationToken cancellationToken);

        Task<DataResult<ProductResponse>>
            UpdateProductAsync(UpdateProductSpecification specification, CancellationToken cancellationToken);

        Task<Result>
            DeleteProductAsync(DeleteProductSpecification specification, CancellationToken cancellationToken);

        Task<Result>
            RestoreProductAsync(RestoreProductSpecification specification, CancellationToken cancellationToken);
    }
}
