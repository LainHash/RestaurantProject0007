using System.Linq.Expressions;

namespace Restaurant.Domain.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; protected set; }

        public List<Expression<Func<T, object>>> Includes { get; }
            = new();

        public List<string> IncludeStrings { get; }
            = new();

        public List<Func<IQueryable<T>, IQueryable<T>>> IncludeAggregators { get; }
            = new();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public bool IgnoreQueryFilters { get; private set; }

        protected void AddIgnoreQueryFilters()
        {
            IgnoreQueryFilters = true;
        }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddIncludeAggregator(Func<IQueryable<T>, IQueryable<T>> aggregator)
        {
            IncludeAggregators.Add(aggregator);
        }

        protected void ApplyOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
