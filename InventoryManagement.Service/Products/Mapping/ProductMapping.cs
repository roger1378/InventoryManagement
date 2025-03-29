using AutoMapper;
using InventoryManagement.Repository.Model;
using InventoryManagement.Service.Products.Dto;

namespace InventoryManagement.Service.Products.Mapping;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductResponseDto>()
            .ReverseMap();

        CreateMap<ProductResponseDto, Product>()
            .ReverseMap();

        CreateMap<ProductCreateRequestDto, Product>()
                .ReverseMap();

        CreateMap<ProductUpdateRequestDto, Product>();
    }
}
