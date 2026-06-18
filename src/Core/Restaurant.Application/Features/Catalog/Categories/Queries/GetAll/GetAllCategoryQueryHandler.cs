using Restaurant.Application.Core.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;

public class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryQuery, DataResult<IEnumerable<CategoryResponse>>>
{
    private readonly IRepository<Category> _categoryRepository;

    public GetAllCategoryQueryHandler(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<DataResult<IEnumerable<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
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
