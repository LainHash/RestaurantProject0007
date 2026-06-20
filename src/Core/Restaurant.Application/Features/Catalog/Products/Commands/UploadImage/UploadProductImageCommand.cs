using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Contracts.DTOs.Catalog.Misc;

namespace Restaurant.Application.Features.Catalog.Products.Commands.UploadImage
{
    /// <summary>
    /// Command upload ảnh cho product.
    /// FileStream và FileName được tách riêng khỏi Contracts để tránh dependency vào ASP.NET.
    /// </summary>
    public record UploadProductImageCommand(
        Guid ProductId,
        Stream FileStream,
        string FileName,
        UploadImageRequest Metadata
    ) : ICommand<DataResult<UploadImageResponse>>;
}
