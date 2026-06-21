using Microsoft.Extensions.Logging;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Products.Commands.UploadImage;
using Restaurant.Application.Services;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Misc;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Misc;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Domain.Repositories.Misc;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    public class ImageService : IImageService
    {
        private const int MaxImagesPerProduct = 5;

        private readonly IImageRepository       _imageRepository;
        private readonly IProductRepository     _productRepository;
        private readonly ICloudinaryService     _cloudinaryService;
        private readonly IUnitOfWork            _unitOfWork;
        private readonly ILogger<ImageService>  _logger;

        public ImageService(
            IImageRepository      imageRepository,
            IProductRepository    productRepository,
            ICloudinaryService    cloudinaryService,
            IUnitOfWork           unitOfWork,
            ILogger<ImageService> logger)
        {
            _imageRepository    = imageRepository;
            _productRepository  = productRepository;
            _cloudinaryService  = cloudinaryService;
            _unitOfWork         = unitOfWork;
            _logger             = logger;
        }

        public async Task<DataResult<UploadImageResponse>> UploadProductImageAsync(
            UploadProductImageSpecification specification,
            CancellationToken cancellationToken)
        {
            // 1. Kiểm tra product tồn tại
            var product = await _productRepository.GetByIdAsync(specification.ProductId, cancellationToken);
            if (product is null)
            {
                return DataResult<UploadImageResponse>.Fail(
                    Messages<Product>.NotFound, HttpStatusCode.NotFound);
            }

            // 2. Kiểm tra giới hạn 5 ảnh / product
            var currentCount = await _imageRepository.CountByProductIdAsync(
                specification.ProductId, cancellationToken);

            if (currentCount >= MaxImagesPerProduct)
            {
                return DataResult<UploadImageResponse>.Fail(
                    $"Product đã đạt giới hạn tối đa {MaxImagesPerProduct} ảnh.",
                    HttpStatusCode.UnprocessableEntity);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // 3. Upload lên Cloudinary
                var uploadResult = await _cloudinaryService.UploadAsync(
                    specification.FileStream,
                    specification.FileName,
                    cancellationToken: cancellationToken);

                if (!uploadResult.IsSuccess)
                {
                    return DataResult<UploadImageResponse>.Fail(
                        $"Upload Cloudinary thất bại: {uploadResult.ErrorMessage}",
                        HttpStatusCode.BadGateway);
                }

                // 4. Nếu ảnh mới là primary → unset ảnh primary cũ
                if (specification.Metadata.IsPrimary)
                {
                    await _imageRepository.UnsetPrimaryAsync(specification.ProductId, cancellationToken);
                }

                // 5. Tạo Image entity
                var image = new Image
                {
                    AltText     = specification.Metadata.AltText ?? string.Empty,
                    Url         = uploadResult.Url,
                    StoragePath = uploadResult.StoragePath,
                    FileSize    = uploadResult.FileSize,
                    ContentType = uploadResult.ContentType,
                    IsPrimary   = specification.Metadata.IsPrimary,
                };

                await _imageRepository.AddAsync(image, cancellationToken);

                // 6. Tạo ProductImage join entity
                var productImage = new ProductImage
                {
                    ProductId    = specification.ProductId,
                    Image        = image,
                    DisplayOrder = currentCount + 1,
                };

                await _imageRepository.AddProductImageAsync(productImage, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var response = new UploadImageResponse
                {
                    ImageId      = image.Id,
                    Url          = image.Url,
                    PublicId     = uploadResult.PublicId,
                    AltText      = image.AltText,
                    IsPrimary    = image.IsPrimary,
                    DisplayOrder = productImage.DisplayOrder,
                    FileSize     = image.FileSize,
                    ContentType  = image.ContentType,
                };

                return DataResult<UploadImageResponse>.Success(
                    response,
                    Messages<Image>.AddSuccess,
                    HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Lỗi khi upload ảnh cho product {ProductId}", specification.ProductId);
                return DataResult<UploadImageResponse>.Fail(
                    Messages<Image>.AddError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
