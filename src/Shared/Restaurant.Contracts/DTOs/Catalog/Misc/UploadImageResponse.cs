namespace Restaurant.Contracts.DTOs.Catalog.Misc
{
    public class UploadImageResponse
    {
        public Guid   ImageId      { get; set; }
        public string Url          { get; set; } = string.Empty;
        public string PublicId     { get; set; } = string.Empty;
        public string AltText      { get; set; } = string.Empty;
        public bool   IsPrimary    { get; set; }
        public int    DisplayOrder { get; set; }
        public long   FileSize     { get; set; }
        public string ContentType  { get; set; } = string.Empty;
    }
}
