using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(Guid Id) : ICommand<Result>;
}
