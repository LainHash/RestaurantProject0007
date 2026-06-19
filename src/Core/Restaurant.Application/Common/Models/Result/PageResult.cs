using Restaurant.Application.Constants;
using System.Net;

namespace Restaurant.Application.Common.Models.Result
{
    public class PageResult<T> : DataResult<T> where T : class
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PageResult(T data, int totalItems, int totalPages, int page = PageConstants.Page, int pageSize = PageConstants.PageSize)
            : base(data)
        {
            TotalItems = totalItems;
            TotalPages = totalPages;
            Page = page;
            PageSize = pageSize;
        }

        public PageResult(T data, string message, int totalItems, int totalPages, int page = PageConstants.Page, int pageSize = PageConstants.PageSize)
            : this(data, totalItems, totalPages, page, pageSize)
        {
            Message = message;
        }

        public PageResult(T data, bool isSucceed, string message, HttpStatusCode statusCode, int totalItems, int totalPages, int page = PageConstants.Page, int pageSize = PageConstants.PageSize)
            : this(data, totalItems, totalPages, page, pageSize)
        {
            IsSucceed = isSucceed;
            Message = message;
            StatusCode = (int)statusCode;
        }

        public PageResult(bool isSucceed, string message, HttpStatusCode statusCode) : base(isSucceed, message, statusCode)
        {
        }

        public static PageResult<T> Success(T data, int totalItems, int page, int pageSize, string message = "Success", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            return new PageResult<T>(data, true, message, statusCode, totalItems, totalPages, page, pageSize);
        }
    }
}
