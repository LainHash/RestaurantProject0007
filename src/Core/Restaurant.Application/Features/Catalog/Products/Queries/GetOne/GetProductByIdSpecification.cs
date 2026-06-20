using Restaurant.Domain.Entities.Catalog;
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

            Criteria = p => p.Id == request.Id;
        }
    }
}
