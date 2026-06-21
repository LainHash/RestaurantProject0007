using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Features.Catalog.Products.Commands.UploadImage;
using Restaurant.Contracts.DTOs.Catalog.Misc;

namespace Restaurant.Application.Services.Catalog
{
    public interface IImageService
    {
        Task<DataResult<UploadImageResponse>> UploadProductImageAsync(
            UploadProductImageSpecification specification,
            CancellationToken cancellationToken);
    }
}
