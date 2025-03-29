using InventoryManagement.Service.Products.Dto;
using MediatR;

namespace InventoryManagement.Service.Products;

public class GetProductByIdQuery : IRequest<ProductResponseDto>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}