using AutoMapper;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Categories.Commands.Delete;
using Restaurant.Application.Features.Catalog.Categories.Commands.Restore;
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
            GetCategoriesAsync(GetAllCategorySpecification specification, CancellationToken cancellationToken = default)
        {
            var page = (specification.Skip / specification.Take) + 1;

            var totalItems = await _categoryRepository.CountAsync(specification, cancellationToken);
            var categories = await _categoryRepository.GetAllAsync(specification, cancellationToken);

            var response = _mapper.Map<List<CategoryResponse>>(categories);
            return PageResult<IEnumerable<CategoryResponse>>
                .Success(response, totalItems, page, specification.Take, Messages<Category>.GetAllSuccess);
        }

        public async Task<DataResult<CategoryResponse>>
            CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            if(await _categoryRepository.IsNameUniqueAsync(request.Name, cancellationToken))
            {
                return DataResult<CategoryResponse>
                    .Fail(Messages<Category>.AddError, HttpStatusCode.Conflict);
            }
            
            var category = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return DataResult<CategoryResponse>
                .Success(response, Messages<Category>.AddSuccess, HttpStatusCode.Created);
        }

        public async Task<DataResult<CategoryResponse>> 
            UpdateCategoryAsync(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken = default)
        {
            if (await _categoryRepository.IsNameUniqueAsync(request.Name, cancellationToken, id))
            {
                return DataResult<CategoryResponse>
                    .Fail(Messages<Category>.UpdateError, HttpStatusCode.Conflict);
            }

            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category is null)
            {
                return DataResult<CategoryResponse>
                    .Fail(Messages<Category>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(request, category);

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return DataResult<CategoryResponse>
                .Success(response, Messages<Category>.UpdateSuccess, HttpStatusCode.OK);
        }

        public async Task<Result> 
            DeleteCategoryAsync(DeleteCategorySpecification specification, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result
                    .Fail(Messages<Category>.NotFound, HttpStatusCode.NotFound);
            }

            if (category.IsDeleted)
            {
                return Result
                    .Fail(Messages<Category>.AlreadyDeleted, HttpStatusCode.Conflict);
            }

            category.Delete();

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return Result
                .Success(Messages<Category>.DeleteSuccess, HttpStatusCode.OK);
        }

        public async Task<Result> 
            RestoreCategoryAsync(RestoreCategorySpecification specification, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByIdAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result
                    .Fail(Messages<Category>.NotFound, HttpStatusCode.NotFound);
            }

            if (!category.IsDeleted)
            {
                return Result
                    .Fail(Messages<Category>.NotYetDeleted, HttpStatusCode.Conflict);
            }

            category.Restore();

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync(cancellationToken);

            return Result
                .Success(Messages<Category>.RestoreSuccess, HttpStatusCode.OK);
        }

        
    }
}
