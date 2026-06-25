namespace Restaurant.BlazorApp.Common.Models.Result
{
    public class DataResult<T> : Result
    {
        public T? Data { get; set; }
    }
}
