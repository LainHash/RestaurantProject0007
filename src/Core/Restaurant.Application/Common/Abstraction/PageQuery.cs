using Restaurant.Application.Common.Enums;
using Restaurant.Application.Constants;

namespace Restaurant.Application.Common.Abstraction
{
    public abstract record PageQuery
    {
        public string? Keyword { get; init; }
        public string SortBy { get; init; } = nameof(SortType.CreatedAtDesc);
        public int Page { get; init; } = PageConstants.Page;
        public int PageSize { get; init; } = PageConstants.PageSize;
    }
}
