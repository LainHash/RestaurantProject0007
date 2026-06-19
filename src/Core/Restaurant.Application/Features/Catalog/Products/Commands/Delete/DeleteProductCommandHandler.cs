using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Result>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productService.DeleteProductAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
