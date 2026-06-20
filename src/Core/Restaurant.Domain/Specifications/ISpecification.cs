using System.Linq.Expressions;

namespace Restaurant.Domain.Specifications
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        List<Func<IQueryable<T>, IQueryable<T>>> IncludeAggregators { get; }

        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }

        public bool IgnoreQueryFilters { get; }
    }
}
