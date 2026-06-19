using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : ICommand<Result>;
}
