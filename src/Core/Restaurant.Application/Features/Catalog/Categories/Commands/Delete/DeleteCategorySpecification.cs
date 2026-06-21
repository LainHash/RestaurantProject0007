using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Delete
{
    public class DeleteCategorySpecification : BaseSpecification<Category>
    {
        public DeleteCategorySpecification(DeleteCategoryCommand request)
        {
            Criteria = p => p.Id == request.Id;
            AddIgnoreQueryFilters();
        }
    }
}
