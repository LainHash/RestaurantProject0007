using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Messaging;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Misc;

namespace Restaurant.Application.Features.Catalog.Products.Commands.UploadImage
{
    public class UploadProductImageCommandHandler
        : ICommandHandler<UploadProductImageCommand, DataResult<UploadImageResponse>>
    {
        private readonly IImageService _imageService;

        public UploadProductImageCommandHandler(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<DataResult<UploadImageResponse>> Handle(
            UploadProductImageCommand request,
            CancellationToken cancellationToken)
        {
            var specification = new UploadProductImageSpecification(request);
            return await _imageService.UploadProductImageAsync(specification, cancellationToken);
        }
    }
}
