namespace Restaurant.Application.Core.Result
{
    public class DataResult<T> : Result
    {
        public T? Data { get; set; }

        public DataResult(T data)
        {
            Data = data;
            IsSucceed = true;
            StatusCode = 200;
        }

        public DataResult(T data, string message) : this(data)
        {
            Message = message;
        }

        public DataResult(bool isSucceed, string message, int statusCode)
        {
            IsSucceed = isSucceed;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
