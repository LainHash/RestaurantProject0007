using AutoMapper;
using Restaurant.Contracts.DTOs.Catalog.Misc;
using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Persistence.Mapping.Catalog
{
    public class ProductMP : Profile
    {
        public ProductMP()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.ProductStock != null ? src.ProductStock.UnitPrice : 0))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.ProductStock != null ? src.ProductStock.Unit : string.Empty))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.ProductStock != null ? src.ProductStock.StockQuantity : 0))
                .ForMember(dest => dest.PrimaryImage, opt => opt.MapFrom(src =>
                    src.ProductImages
                       .Where(pi => pi.Image != null && pi.Image.IsPrimary)
                       .OrderBy(pi => pi.DisplayOrder)
                       .Select(pi => new ImageResponse
                       {
                           Url = pi.Image.Url,
                           AltText = pi.Image.AltText,
                           DisplayOrder = pi.DisplayOrder,
                           IsPrimary = true
                       })
                       .FirstOrDefault()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
                    src.ProductImages
                       .Where(pi => pi.Image != null)
                       .OrderBy(pi => pi.DisplayOrder)
                       .Select(pi => new ImageResponse
                       {
                           Url = pi.Image.Url,
                           AltText = pi.Image.AltText,
                           DisplayOrder = pi.DisplayOrder,
                           IsPrimary = pi.Image.IsPrimary
                       })));

            CreateMap<CreateProductRequest, Product>();
            CreateMap<CreateProductRequest, ProductStock>();

            CreateMap<UpdateProductRequest, Product>();
            CreateMap<UpdateProductRequest, ProductStock>();
        }
    }
}
