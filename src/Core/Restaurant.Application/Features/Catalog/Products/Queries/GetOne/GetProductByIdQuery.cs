using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetOne
{
    public record GetProductByIdQuery(Guid Id) : IQuery<DataResult<ProductResponse>>;
}
