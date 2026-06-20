using Microsoft.EntityFrameworkCore;
using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductSpecification : BaseSpecification<Product>
    {
        public UpdateProductRequest RequestBody { get; set; }
        public UpdateProductSpecification(UpdateProductCommand request)
        {
            // Includes: eager load navigation properties
            AddInclude(p => p.Category);
            AddInclude(p => p.ProductStock);
            AddIncludeAggregator(q => q.Include(p => p.ProductImages)
                                       .ThenInclude((ProductImage pi) => pi.Image));

            Criteria = p => p.Id == request.Id;
            RequestBody = request.UpdateProductRequest;
        }
    }
}
