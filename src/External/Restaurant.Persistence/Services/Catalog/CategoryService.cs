using AutoMapper;
using Restaurant.Application.Common.Result;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog;
using Restaurant.Domain.Repositories.Catalog;

namespace Restaurant.Persistence.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<DataResult<IEnumerable<CategoryResponse>>> GetCategoriesAsync(GetAllCategoryQuery request, CancellationToken cancellationToken = default)
        {
            var categories = _categoryRepository.GetAllAsync()
            .Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToList();
            return new DataResult<IEnumerable<CategoryResponse>>(categories);
        }
    }
}
