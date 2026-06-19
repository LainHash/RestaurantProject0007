using AutoMapper;
using Restaurant.Application.Common.Enums;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Categories;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using System.Net;

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

        public async Task<PageResult<IEnumerable<CategoryResponse>>>
            GetCategoriesAsync(GetAllCategoryQuery request, CancellationToken cancellationToken = default)
        {
            var query = _categoryRepository.GetAllAsync();

            query = Filtering(query, request);

            query = Paginating(query, request, out int totalItems);

            var categories = _mapper.Map<List<CategoryResponse>>(query.ToList());

            return PageResult<IEnumerable<CategoryResponse>>
                .Success(categories, totalItems, request.Page, request.PageSize, Messages<Category>.GetAllSuccess);
        }

        public async Task<DataResult<CategoryResponse>>
            CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return DataResult<CategoryResponse>
                .Success(_mapper.Map<CategoryResponse>(category), Messages<Category>.AddSuccess, HttpStatusCode.Created);
        }

        public async Task<DataResult<CategoryResponse>> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return DataResult<CategoryResponse>
                    .Fail(Messages<Category>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(request, category);

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return DataResult<CategoryResponse>
                .Success(_mapper.Map<CategoryResponse>(category), Messages<Category>.UpdateSuccess, HttpStatusCode.OK);
        }

        public async Task<Result> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Messages<Category>.NotFound, HttpStatusCode.NotFound);
            }

            category.Delete();

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return Result
                .Success(Messages<Category>.DeleteSuccess, HttpStatusCode.OK);
        }

        public async Task<Result> RestoreCategoryAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Messages<Category>.NotFound, HttpStatusCode.NotFound);
            }

            category.Restore();

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return Result
                .Success(Messages<Category>.RestoreSuccess, HttpStatusCode.OK);
        }

        private IQueryable<Category> Filtering(IQueryable<Category> query, GetAllCategoryQuery request)
        {
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(c => c.Name.Contains(request.Keyword, StringComparison.CurrentCultureIgnoreCase));
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

            return query;
        }

        private IQueryable<Category> Paginating(IQueryable<Category> query, GetAllCategoryQuery request, out int totalItems)
        {
            totalItems = query.Count();

            query = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            return query;
        }

        
    }
}
