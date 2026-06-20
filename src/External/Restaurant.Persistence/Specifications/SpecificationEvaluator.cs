using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Specifications;

namespace Restaurant.Persistence.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> spec, bool applyPaging = true) where T : class
        {
            if(spec.IgnoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            // 1. Áp dụng bộ lọc Điều kiện (Criteria / Where)
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // 2. Áp dụng các lệnh nạp dữ liệu liên quan (Includes - expression-based)
            if (spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // 3. Áp dụng Includes dạng chuỗi (string-based / ThenInclude phức tạp)
            if (spec.IncludeStrings != null)
            {
                query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            }

            // 4. Áp dụng Includes dạng strongly-typed ThenInclude (IncludeAggregators)
            if (spec.IncludeAggregators != null)
            {
                query = spec.IncludeAggregators.Aggregate(query, (current, aggregator) => aggregator(current));
            }

            // 5. Áp dụng Sắp xếp (OrderBy / OrderByDescending)
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // 5. Áp dụng Phân trang (Paging) - Phải đặt SAU OrderBy để đảm bảo chính xác
            if (applyPaging && spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
    }
}
