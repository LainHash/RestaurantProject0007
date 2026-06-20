using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductSpecification : BaseSpecification<Product>
    {
        public CreateProductRequest RequestBody { get; set; }
        public CreateProductSpecification(CreateProductCommand request)
        {
            RequestBody = request.CreateProductRequest;
        }
    }
}
