using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Enums;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductSpecification : BaseSpecification<Product>
    {
        public GetAllProductSpecification(GetAllProductQuery request)
        {
            // Includes: eager load navigation properties
            AddInclude(p => p.Category);
            AddInclude(p => p.ProductStock);
            AddIncludeAggregator(q => q.Include(p => p.ProductImages)
                                       .ThenInclude((ProductImage pi) => pi.Image));

            // Criteria: kết hợp filter Keyword và CategoryName
            bool hasKeyword = !string.IsNullOrWhiteSpace(request.Keyword);
            bool hasCategoryName = !string.IsNullOrWhiteSpace(request.CategoryName);

            if (hasKeyword && hasCategoryName)
            {
                Criteria = p => p.Name.ToLower()
                                .Contains(request.Keyword!.ToLower()) &&
                                p.Category.Name.ToLower()
                                .Contains(request.CategoryName!.ToLower());
            }
            else if (hasKeyword)
            {
                Criteria = p => p.Name.Contains(request.Keyword!);
            }
            else if (hasCategoryName)
            {
                Criteria = p => p.Category.Name.Contains(request.CategoryName!);
            }

            // OrderBy: sắp xếp theo SortBy
            switch (request.SortBy)
            {
                case nameof(SortType.CreatedAtAsc):
                    ApplyOrderBy(p => p.CreatedAt);
                    break;
                case nameof(SortType.NameAsc):
                    ApplyOrderBy(p => p.Name);
                    break;
                case nameof(SortType.NameDesc):
                    ApplyOrderByDescending(p => p.Name);
                    break;
                case nameof(SortType.PriceAsc):
                    ApplyOrderBy(p => p.ProductStock.UnitPrice);
                    break;
                case nameof(SortType.PriceDesc):
                    ApplyOrderByDescending(p => p.ProductStock.UnitPrice);
                    break;
                default:
                    ApplyOrderByDescending(p => p.CreatedAt);
                    break;
            }

            // Paging
            ApplyPaging((request.Page - 1) * request.PageSize, request.PageSize);
        }
    }
}
