using Restaurant.Application.Common.Abstraction;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;

public record GetAllCategoryQuery() : PageQuery, IQuery<PageResult<IEnumerable<CategoryResponse>>>;
