using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductQueryHandler : IQueryHandler<GetAllProductQuery, PageResult<IEnumerable<ProductResponse>>>
    {
        private readonly IProductService _productService;
        public GetAllProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<PageResult<IEnumerable<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var response = await _productService.GetProductsAsync(request, cancellationToken);
            return response;
        }
    }
}
