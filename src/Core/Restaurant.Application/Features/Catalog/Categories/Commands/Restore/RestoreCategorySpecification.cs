using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public class RestoreCategorySpecification : BaseSpecification<Category>
    {
        public RestoreCategorySpecification(RestoreCategoryCommand request)
        {
            Criteria = p => p.Id == request.Id;
            AddIgnoreQueryFilters();
        }
    }
}
