using MediatR;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Categories;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Create
{
    public record CreateCategoryCommand(CreateCategoryRequest CreateCategoryRequest) : ICommand<DataResult<CategoryResponse>>;
}
