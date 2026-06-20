using Restaurant.Contracts.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    public class UpdateCategorySpecification : BaseSpecification<Category>
    {
        public UpdateCategoryRequest RequestBody { get; set; }
        public UpdateCategorySpecification(UpdateCategoryCommand request)
        {
            Criteria = c => c.Id == request.Id;
            RequestBody = request.UpdateCategoryRequest;
        }
    }
}
