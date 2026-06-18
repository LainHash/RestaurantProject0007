using System.Net;

namespace Restaurant.Application.Common.Result
{
    public class DataResult<T> : Result
    {
        public T? Data { get; set; }

        public DataResult(T data)
        {
            Data = data;
            IsSucceed = true;
            StatusCode = (int)HttpStatusCode.OK;
        }

        public DataResult(T data, string message) : this(data)
        {
            Message = message;
        }

        public DataResult(bool isSucceed, string message, HttpStatusCode statusCode)
        {
            IsSucceed = isSucceed;
            Message = message;
            StatusCode = (int)statusCode;
        }
    }
}
