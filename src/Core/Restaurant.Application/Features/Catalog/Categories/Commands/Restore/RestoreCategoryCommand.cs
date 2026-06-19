using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public record RestoreCategoryCommand(Guid Id) : ICommand<Result>;
}
