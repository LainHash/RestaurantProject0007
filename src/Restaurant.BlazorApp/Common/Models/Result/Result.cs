namespace Restaurant.BlazorApp.Common.Models.Result
{
    public class Result
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
