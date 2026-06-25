namespace Restaurant.BlazorApp.Common.Models.Result
{
    public class PageResult<T> : DataResult<T> where T : class
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
