using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public record UpdateProductCommand(Guid Id, UpdateProductRequest UpdateProductRequest) : ICommand<DataResult<ProductResponse>>;
}
