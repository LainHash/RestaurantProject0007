using Restaurant.Application.Common.Abstraction;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public record GetAllProductQuery : PageQuery, IQuery<PageResult<IEnumerable<ProductResponse>>>
    {
        public string? CategoryName { get; init; }
    }
}
