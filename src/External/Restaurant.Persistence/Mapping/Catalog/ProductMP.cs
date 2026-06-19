using AutoMapper;
using Restaurant.Contracts.DTOs.Catalog.Products;
using Restaurant.Domain.Entities.Catalog;

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
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.ProductStock != null ? src.ProductStock.StockQuantity : 0));
        }
    }
}
