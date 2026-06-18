using Restaurant.Application.Core.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;

public record GetAllCategoryQuery : IQuery<DataResult<IEnumerable<CategoryResponse>>>;
