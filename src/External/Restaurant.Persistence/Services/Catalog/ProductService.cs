using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Services;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Repositories.Catalog;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageResult<IEnumerable<ProductResponse>>>
            GetProductsAsync(GetAllProductSpecification specification, CancellationToken cancellationToken)
        {

            var totalItems = await _productRepository.CountAsync(specification, cancellationToken);
            var products = await _productRepository.GetAllAsync(specification, cancellationToken);

            var response = _mapper.Map<List<ProductResponse>>(products);
            return PageResult<IEnumerable<ProductResponse>>
                .Success(response, totalItems, (specification.Skip / specification.Take) + 1, specification.Take, Messages<Product>.GetAllSuccess);
        }


        public async Task<DataResult<ProductResponse>>
            GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

            if (product is null)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ProductResponse>(product);
            return DataResult<ProductResponse>
                .Success(response, Messages<Product>.GetByIdSuccess);
        }


        public async Task<DataResult<ProductResponse>>
            CreateProductAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var product = _mapper.Map<Product>(request);

                var productStock = _mapper.Map<ProductStock>(request);
                product.ProductStock = productStock;

                await _productRepository.AddAsync(product);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var response = _mapper.Map<ProductResponse>(product);
                return DataResult<ProductResponse>
                    .Success(response, Messages<Product>.AddSuccess, HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Xảy ra lỗi khi tạo Product. Request data: {@Request}", request);
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.AddError, HttpStatusCode.InternalServerError);
            }
        }


        public async Task<DataResult<ProductResponse>>
            UpdateProductAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

            if (product is null)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.NotFound, HttpStatusCode.NotFound);
            }

            _mapper.Map(request, product);
            _mapper.Map(request, product.ProductStock);

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductResponse>(product);
            return DataResult<ProductResponse>
                .Success(response, Messages<Product>.UpdateSuccess, HttpStatusCode.OK);
        }


        public async Task<Result>
            DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

            if (product is null)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (product.IsDeleted)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.DeleteError, HttpStatusCode.Conflict);
            }

            product.Delete();
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            var response = _mapper.Map<ProductResponse>(product);
            return DataResult<ProductResponse>
                .Success(response, Messages<Product>.DeleteSuccess, HttpStatusCode.OK);
        }


        public async Task<Result>
            RestoreProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

            if (product is null)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.NotFound, HttpStatusCode.NotFound);
            }

            if (!product.IsDeleted)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.RestoreError, HttpStatusCode.Conflict);
            }

            product.Restore();
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            var response = _mapper.Map<ProductResponse>(product);
            return DataResult<ProductResponse>
                .Success(response, Messages<Product>.RestoreSuccess, HttpStatusCode.OK);
        }
    }
}
