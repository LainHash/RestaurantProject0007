namespace Restaurant.Contracts.DTOs.Catalog.Misc
{
    /// <summary>
    /// Dữ liệu metadata ảnh — file stream được xử lý ở Controller trước khi tạo Command.
    /// </summary>
    public class UploadImageRequest
    {
        public string? AltText  { get; set; }
        public bool    IsPrimary { get; set; }
    }
}
