using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetOne
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, DataResult<ProductResponse>>
    {
        private readonly IProductService _productService;
        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<DataResult<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetProductByIdSpecification(request);
            var response = await _productService.GetProductByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
