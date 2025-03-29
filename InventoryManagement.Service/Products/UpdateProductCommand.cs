using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class UpdateProductCommand : IRequest<ProductResponseDto>
{
    public ProductUpdateRequestDto ProductUpdateRequestDto { get; set; }

    public UpdateProductCommand(ProductUpdateRequestDto productUpdateRequestDto)
    {
        ProductUpdateRequestDto = productUpdateRequestDto;
    }
}
