using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetOne
{
    public class GetProductByIdSpecification : BaseSpecification<Product>
    {
        public GetProductByIdSpecification(GetProductByIdQuery request)
        {
            // Includes: eager load navigation properties
            AddInclude(p => p.Category);
            AddInclude(p => p.ProductStock);
            AddIncludeAggregator(q => q.Include(p => p.ProductImages)
                                       .ThenInclude((ProductImage pi) => pi.Image));

            Criteria = p => p.Id == request.Id;
        }
    }
}
