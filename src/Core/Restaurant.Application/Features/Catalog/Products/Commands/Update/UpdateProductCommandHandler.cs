using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, DataResult<ProductResponse>>
    {
        private readonly IProductService _productService;
        
        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<DataResult<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productService.UpdateProductAsync(request.Id, request.UpdateProductRequest, cancellationToken);
            return response;
        }
    }
}
