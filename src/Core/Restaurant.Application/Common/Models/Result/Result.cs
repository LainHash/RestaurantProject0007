using System.Net;

namespace Restaurant.Application.Common.Models.Result
{
    public class Result
    {
        public bool IsSucceed { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public Result() { }

        public Result(bool isSucceed, string? message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            IsSucceed = isSucceed;
            Message = message;
            StatusCode = (int)statusCode;
        }

        public static Result Success(string? message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result(true, message, statusCode);
        }

        public static Result Fail(string? message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Result(false, message, statusCode);
        }
    }


}
