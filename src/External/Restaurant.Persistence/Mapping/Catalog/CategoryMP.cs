using AutoMapper;
using Restaurant.Contracts.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Persistence.Mapping.Catalog
{
    public class CategoryMP : Profile
    {
        public CategoryMP()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>();
        }
    }
}
