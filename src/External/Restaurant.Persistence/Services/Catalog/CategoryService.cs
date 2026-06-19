using AutoMapper;
using Restaurant.Application.Common.Enums;
using Restaurant.Application.Common.Models.Result;
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

        public async Task<PageResult<IEnumerable<CategoryResponse>>> GetCategoriesAsync(GetAllCategoryQuery request, CancellationToken cancellationToken = default)
        {
            var query = _categoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.Keyword.ToLower()));
            }

            switch (request.SortBy)
            {
                case nameof(SortType.CreatedAtAsc):
                    query = query.OrderBy(p => p.CreatedAt);
                    break;
                case nameof(SortType.NameAsc):
                    query = query.OrderBy(p => p.Name);
                    break;
                case nameof(SortType.NameDesc):
                    query = query.OrderByDescending(p => p.Name);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / request.PageSize);

            var categories = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            return new PageResult<IEnumerable<CategoryResponse>>(categories, totalItems, totalPages, request.Page, request.PageSize);
        }
    }
}
