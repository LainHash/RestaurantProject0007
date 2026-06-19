using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    public record RestoreProductCommand(Guid Id) : ICommand<Result>;
}
