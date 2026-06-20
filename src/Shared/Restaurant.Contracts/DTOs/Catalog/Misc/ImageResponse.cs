namespace Restaurant.Contracts.DTOs.Catalog.Misc
{
    public class ImageResponse
    {
        public string Url { get; set; } = string.Empty;
        public string? AltText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPrimary { get; set; }
    }
}
