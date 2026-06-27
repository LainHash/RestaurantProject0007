using Restaurant.Application.Common.Enums;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll
{
    public class GetAllCategorySpecification : BaseSpecification<Category>
    {
        public GetAllCategorySpecification(GetAllCategoryQuery request)
        {
            // Criteria: filter theo Keyword (tên category)
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                Criteria = c => c.Name.ToLower()
                                    .Contains(request.Keyword.ToLower());
            }

            // OrderBy: sắp xếp theo SortBy
            switch (request.SortBy)
            {
                case nameof(SortType.CreatedAtAsc):
                    ApplyOrderBy(c => c.CreatedAt);
                    break;
                case nameof(SortType.NameAsc):
                    ApplyOrderBy(c => c.Name);
                    break;
                case nameof(SortType.NameDesc):
                    ApplyOrderByDescending(c => c.Name);
                    break;
                default:
                    ApplyOrderByDescending(c => c.CreatedAt);
                    break;
            }

            // Paging
            ApplyPaging((request.Page - 1) * request.PageSize, request.PageSize);
        }
    }
}
