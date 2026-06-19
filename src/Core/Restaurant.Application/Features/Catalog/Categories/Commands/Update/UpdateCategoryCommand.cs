using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    public record UpdateCategoryCommand(Guid Id, UpdateCategoryRequest UpdateCategoryRequest) : ICommand<DataResult<CategoryResponse>>;
}
