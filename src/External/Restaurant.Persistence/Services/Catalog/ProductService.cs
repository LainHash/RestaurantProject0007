using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Enums;
using Restaurant.Application.Common.Models.Result;
using Restaurant.Application.Constants;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Services.Catalog;
using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using System.Net;

namespace Restaurant.Persistence.Services.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<IEnumerable<ProductResponse>>> GetProductsAsync(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _productRepository.GetAllAsync()
                .Include(p => p.Category)
                .Include(p => p.ProductStock);

            query = Filtering(query, request);

            query = Paginating(query, request, out int totalItems);

            var response = _mapper.Map<List<ProductResponse>>(query.ToList());
            return PageResult<IEnumerable<ProductResponse>>
                .Success(response, totalItems, request.Page, request.PageSize, Messages<Product>.GetAllSuccess);
        }

        public async Task<DataResult<ProductResponse>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAllAsync(cancellationToken)
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product == null)
            {
                return DataResult<ProductResponse>
                    .Fail(Messages<Product>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ProductResponse>(product);
            return DataResult<ProductResponse>
                .Success(response, Messages<Product>.GetByIdSuccess);
        }

        private IQueryable<Product> Filtering(IQueryable<Product> query, GetAllProductQuery request)
        {
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.Name.Contains(request.Keyword, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(request.CategoryName))
            {
                query = query.Where(p => p.Category.Name.Contains(request.CategoryName, StringComparison.CurrentCultureIgnoreCase));
            }

            switch (request.SortBy)
            {
                case nameof(SortType.CreatedAtAsc):
                    query = query.OrderBy(p => p.CreatedAt);
                    break;
                case nameof(SortType.NameAsc):
                    query = query.OrderBy(p => p.Name);
                    break;
                case nameof(SortType.NameDesc):
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case nameof(SortType.PriceAsc):
                    query = query.OrderBy(p => p.ProductStock.UnitPrice);
                    break;
                case nameof(SortType.PriceDesc):
                    query = query.OrderByDescending(p => p.ProductStock.UnitPrice);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            return query;
        }

        private IQueryable<Product> Paginating(IQueryable<Product> query, GetAllProductQuery request, out int totalItems)
        {
            totalItems = query.Count();

            query = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            return query;
        }
    }
}
