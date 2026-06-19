using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    public class RestoreProductCommandHandler : ICommandHandler<RestoreProductCommand, Result>
    {
        private readonly IProductService _productService;

        public RestoreProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Result> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productService.RestoreProductAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
