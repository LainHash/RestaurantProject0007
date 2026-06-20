using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    public class RestoreProductSpecification : BaseSpecification<Product>
    {
        public RestoreProductSpecification(RestoreProductCommand request)
        {
            Criteria = p => p.Id == request.Id;
            AddIgnoreQueryFilters();
        }
    }
}
