using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class InsertProductCommand : IRequest<ProductResponseDto>
{
    public ProductCreateRequestDto ProductCreateRequestDto { get; set; }

    public InsertProductCommand(ProductCreateRequestDto productCreateRequestDto)
    {
        ProductCreateRequestDto = productCreateRequestDto;
    }
}