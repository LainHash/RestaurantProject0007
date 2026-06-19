using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public record CreateProductCommand(CreateProductRequest CreateProductRequest) : ICommand<DataResult<ProductResponse>>;
}
