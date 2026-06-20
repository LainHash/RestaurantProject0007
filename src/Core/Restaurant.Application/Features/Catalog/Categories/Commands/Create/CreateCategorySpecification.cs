using Restaurant.Contracts.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Create
{
    public class CreateCategorySpecification : BaseSpecification<Category>
    {
        public CreateCategoryRequest RequestBody { get; set; }
        public CreateCategorySpecification(CreateCategoryCommand request)
        {
            RequestBody = request.CreateCategoryRequest;
        }
    }
}
