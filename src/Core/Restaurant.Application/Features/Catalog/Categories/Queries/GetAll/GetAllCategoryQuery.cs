using Restaurant.Application.Common.Abstraction;
using Restaurant.Application.Common.Enums;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;

public record GetAllCategoryQuery() : PageQuery, IQuery<PageResult<IEnumerable<CategoryResponse>>>;
