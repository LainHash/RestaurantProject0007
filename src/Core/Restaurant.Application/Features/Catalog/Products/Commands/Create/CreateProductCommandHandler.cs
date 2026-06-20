using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, DataResult<ProductResponse>>
    {
        private readonly IProductService _productService;
        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<DataResult<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateProductSpecification(request);
            var response = await _productService.CreateProductAsync(specification, cancellationToken);
            return response;
        }
    }
}
